"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""


# Built-in Imports
import random
from typing import List, Optional
import signal
from datetime import datetime
import os
import shutil
import zipfile
import time
import sys

# Third Party Imports
from bs4 import BeautifulSoup
from loguru import logger
import requests
from alive_progress import alive_bar

# Local Application Imports


class ModpackDownloader:
    """
    This class implements the main functions of the program, downloading and automatic
    management of files in the .minecraft/mods folder.
    """

    def __init__(self):
        self.__mods_folder_path = os.path.join(os.environ["APPDATA"], ".minecraft", "mods")
        self.__old_folder_path = os.path.join(self.__mods_folder_path, ".OLD_MODS")
        self.__removed_files = list()  # List of removed files during the program execution
        # There's no need for a list of added files because we can simply check for what files are in the "mods" folder.


    def start(self) -> None:
        """
        Acts as the main executor of all the main program functions.
        :return:
        """
        os.makedirs(self.__mods_folder_path, exist_ok=True)
        self._secure_old_files()
        self._download_modpack()
        self._show_files()


    def _secure_old_files(self) -> None:
        """
        Secures all the "old" files inside the "mods" folder by moving them
        into an .OLD_MODS/<timestamp> folder, so they don't interfere with the installation of the new mods.
        :return:
        """

        if len(os.listdir(self.__mods_folder_path)) <= 0:
            logger.info("No files found in the mods folder, nothing to secure.")
            return  # Not sure how a length < 0 could happen, but I wouldn't be surprised if it did.

        # Old mods folder to be used. ".OLD_MODS/TIMESTAMP"
        old_mods_folder: str = os.path.join(self.__old_folder_path, str(int(datetime.now().timestamp())))
        os.makedirs(old_mods_folder, exist_ok=True)

        for file in [x for x in os.listdir(self.__mods_folder_path) if x != ".OLD_MODS"]:
            filepath: str = os.path.join(self.__mods_folder_path, file)
            shutil.move(filepath, old_mods_folder)
            self.__removed_files.append(file)
            logger.debug(f"[$] Secured {filepath} in {old_mods_folder}")


    def _download_modpack(self) -> None:
        """
        Downloads the modpack and shows a progress bar for the progress.
        :return:
        """

        download_zip: str = os.path.join(self.__mods_folder_path, "modpack.zip")
        chosen_bar: str = random.choice(["classic", "classic2", "squares", "ruler2", "brackets", "fish"])
        chunk_size: int = int(self._get_panel_setting("DOWNLOAD-CHUNK-SIZE"))

        # Gets the redirect link and opens a request stream to download the content
        # Initialises an alive bar to show completion. This bar will be random within the allowed ones.
        with requests.get(self._get_panel_setting("REDIRECT1"), stream=True) as r, \
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
        for file in [x for x in os.listdir(self.__mods_folder_path) if x != ".OLD_MODS"]:
            logger.info(f"[+] {file}")


    @staticmethod
    def _get_panel_setting(setting: str) -> Optional[str]:
        """
        Returns a value for a given setting in the github gist panel
        :return str setting_value: The value for the specified setting
        """

        data: requests.Response = requests.get("https://gist.github.com/MrKelpy/6443d414004b00a583ab80b8e9187e65")
        soup: BeautifulSoup = BeautifulSoup(data.text, "html.parser")
        lines: List[str] = [y.text for y in soup.find_all(class_="blob-code blob-code-inner js-file-line")]
        setting_value: Optional[str] = [x.split(">>")[1].strip() for x in lines if x.startswith(setting)][0]

        return setting_value


    @staticmethod
    def exit_countdown() -> None:
        """
        Starts a program sigterm countdown from 10s to 1.
        At the end of the countdown, SIGTERM the process.
        :return:
        """

        for i in range(10):
            sys.stdout.write(f"\r[INFO] Exiting in {10-i} ")
            sys.stdout.flush()
            time.sleep(1)

        os.kill(os.getpid(), signal.SIGTERM)
