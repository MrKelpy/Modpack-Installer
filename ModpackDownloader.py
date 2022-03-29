"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack Installer 

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""


# Built-in Imports
from datetime import datetime
import os
import shutil
import zipfile

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
        os.makedirs(self.__mods_folder_path, exist_ok=True)


    def start(self) -> None:
        """
        Acts as the main executor of all the main program functions.
        :return:
        """
        self._secure_old_files()
        self._download_modpack()


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
            logger.info(f"Secured {filepath} in {old_mods_folder}")


    def _download_modpack(self) -> None:
        """
        Downloads the modpack and shows a progress bar for the progress.
        :return:
        """

        download_zip: str = os.path.join(self.__mods_folder_path, "modpack.zip")

        # Gets the redirect link and opens a request stream to download the content
        with requests.get(self.__get_redirect_link(), stream=True) as r:
            r.raise_for_status()
            content_size: int = int(r.headers["content-length"])

            # Initialises an alive bar to show completion
            with alive_bar(content_size//8192+1, force_tty=True, title="Downloading mods", monitor=True,
                           elapsed=False, stats=False, theme="smooth") as bar:

                with open(download_zip, 'wb') as file:
                    # Downloads a moderately sized chunk per iteration, writing it into a zip in the "mods" folder.
                    for chunk in r.iter_content(chunk_size=8192):
                        file.write(chunk)
                        bar()

                    # Finishes the remaining part of the progress bar (Because of precision losses)
                    while bar.current() < (content_size//8192):
                        bar()

        # Extracts the zip contents into the mods folder
        with zipfile.ZipFile(download_zip, 'r') as zip_ref:
            zip_ref.extractall(self.__mods_folder_path)
            logger.info("Extracting modpack files")

        # Removes the modpack zip
        os.remove(download_zip)
        logger.info("Removed residual files")


    @staticmethod
    def __get_redirect_link() -> str:
        """
        Returns the redirection link from the GitHub "gist" to download the mods from.
        :return:
        """

        data: requests.Response = requests.get("https://gist.github.com/MrKelpy/6443d414004b00a583ab80b8e9187e65")
        soup: BeautifulSoup = BeautifulSoup(data.text, "html.parser")
        logger.info("Retrieved the redirection link")

        return soup.find(id="file-txt-LC1").text
