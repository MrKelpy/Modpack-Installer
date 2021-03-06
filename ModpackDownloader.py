"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""


# Built-in Imports
import os
import random
import shutil
import string
import zipfile
from datetime import datetime

# Third Party Imports
import requests
from alive_progress import alive_bar
from loguru import logger

# Local Application Imports
from GeneralUtils import GeneralUtils


class ModpackDownloader:
    """
    This class implements the main functions of the program, downloading and automatic
    management of files in the .minecraft/mods folder.
    """

    def __init__(self):
        self.__minecraft_folder = os.path.join(os.environ["APPDATA"], ".minecraft")
        self.__mods_folder_path = os.path.join(self.__minecraft_folder, "mods")
        self.__old_folder_path = os.path.join(self.__mods_folder_path, ".OLD_MODS")
        self.__removed_files = list()  # List of removed files during the program execution
        # There's no need for a list of added files because we can simply check for what files are in the "mods" folder.


    def start(self) -> None:
        """
        Acts as the main executor of all the main program functions.
        :return:
        """
        os.makedirs(self.__mods_folder_path, exist_ok=True)
        self._secure_old_files(self.__mods_folder_path)
        self._download_modpack()
        self._setup_game_directories()
        self._show_files()


    def _download_modpack(self) -> None:
        """
        Downloads the modpack and shows a progress bar for the progress.
        :return:
        """

        download_zip: str = os.path.join(self.__mods_folder_path, "modpack.zip")
        chosen_bar: str = random.choice(["classic", "classic2", "squares", "ruler2", "brackets", "fish"])
        chunk_size: int = int(GeneralUtils().get_panel_setting("DOWNLOAD-CHUNK-SIZE"))

        # Gets the redirect link and opens a request stream to download the content
        # Initialises an alive bar to show completion. This bar will be random within the allowed ones.
        with requests.get(GeneralUtils().get_panel_setting("REDIRECT1"), stream=True) as r, \
             alive_bar(int(r.headers["content-length"])//chunk_size+1, force_tty=True, title="[INFO] Downloading mods",
                       monitor=False, length=50, elapsed=False, stats=False, bar=chosen_bar, spinner="classic",
                       spinner_length=0) as bar, \
             open(download_zip, 'wb') as file:

            r.raise_for_status()

            # Downloads a moderately sized chunk per iteration, writing it into a zip in the "mods" folder.
            for chunk in r.iter_content(chunk_size=8192):
                file.write(chunk)
                bar()

            # Finishes the remaining part of the progress bar (Because of precision losses)
            while bar.current() < (int(r.headers["content-length"])//chunk_size):
                bar()

        # Extracts the zip contents into the mods folder
        with zipfile.ZipFile(download_zip, 'r') as zip_ref:
            logger.info("Extracting modpack files")
            zip_ref.extractall(self.__mods_folder_path)

        # Removes the modpack zip
        os.remove(download_zip)
        logger.info("Removed residual files")


    def _show_files(self) -> None:
        """
        Displays a list of the file changes that occurred in the "mods" folder
        during the program execution
        :return:
        """
        logger.debug("Displaying file change info")

        # Displays the removed files
        for file in self.__removed_files:
            logger.info(f"[-] {file}")

        # Displays the added files
        for file in [x for x in os.listdir(self.__mods_folder_path) if x != ".OLD_FILES"]:
            logger.info(f"[+] {file}")


    def _secure_old_files(self, path: str, count_removed: bool = True) -> None:
        """
        Secures all the "old" files inside the specified folder by moving them
        into an .OLD_FILES/<timestamp> folder, so they don't interfere with the installation of the new modpack.

        :param str path: The path to the directory to secure the files
        :param bool count_removed: Whether or not to count the files towards the removed files.
        :return:
        """

        if len(os.listdir(path)) <= 0:
            logger.info(f"No files found in {path}, nothing to secure.")
            return  # Not sure how a length < 0 could happen, but I wouldn't be surprised if it did.

        # Old files folder to be used. ".OLD_FILES/TIMESTAMP"
        old_files_folder: str = os.path.join(path, ".OLD_FILES", str(int(datetime.now().timestamp())))
        os.makedirs(old_files_folder, exist_ok=True)

        for file in [x for x in os.listdir(path) if x != ".OLD_FILES"]:
            filepath: str = os.path.join(path, file)
            shutil.move(filepath, old_files_folder)
            if count_removed: self.__removed_files.append(file)
            logger.debug(f"[$] Secured {filepath} in {old_files_folder}")


    def _setup_game_directories(self) -> None:
        """
        Detects any directories that have been extracted from the modpack.zip file
        and sets them up in the .minecraft folder in a similar way as the "mods" folder.
        :return:

        Any directories without characters in their name are probably coremod forge version
        folders, and the old files directory.
        """

        for directory in [x for x in os.listdir(self.__mods_folder_path)
                          if os.path.isdir(os.path.join(self.__mods_folder_path, x)) and x != ".OLD_FILES"]:

            # Ignore directories with no characters in their name
            if not any(char in directory for char in string.ascii_letters): continue

            logger.info(f"Setting up the {directory} folder")
            dst_dirpath: str = os.path.join(self.__minecraft_folder, directory)
            src_dirpath: str = os.path.join(self.__mods_folder_path, directory)
            os.makedirs(dst_dirpath, exist_ok=True)
            logger.debug(f"Ensured the existance of a {dst_dirpath} directory")

            # The "mods" folder is an exception since it gets secured when the program boots up.
            if directory != "mods": self._secure_old_files(dst_dirpath, count_removed=False)

            for file in os.listdir(os.path.join(self.__mods_folder_path, directory)):
                filepath: str = os.path.join(src_dirpath, file)
                shutil.move(filepath, dst_dirpath)
                logger.debug(f"[>] Sent {file} to the {directory} folder")

            os.rmdir(os.path.join(self.__mods_folder_path, directory))

