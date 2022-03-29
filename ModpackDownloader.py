"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack Installer 

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

import logging
import os
import shutil
# Built-in Imports
from datetime import datetime

import requests
# Third Party Imports
from bs4 import BeautifulSoup


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


    def start(self):
        """
        Acts as the main executor of all the main program functions.
        :return:
        """
        self._secure_old_files()


    def _secure_old_files(self) -> None:
        """
        Secures all the "old" files inside the "mods" folder by moving them
        into an .OLD_MODS/<timestamp> folder, so they don't interfere with the installation of the new mods.
        :return:
        """

        if len(os.listdir(self.__mods_folder_path)) <= 0:
            logging.info("No files found in the mods folder, nothing to secure.")
            return  # Not sure how a length < 0 could happen, but I wouldn't be surprised if it did.

        # Old mods folder to be used. ".OLD_MODS/TIMESTAMP"
        old_mods_folder: str = os.path.join(self.__old_folder_path, str(int(datetime.now().timestamp())))
        os.makedirs(old_mods_folder, exist_ok=True)

        for file in [x for x in os.listdir(self.__mods_folder_path) if x != ".OLD_MODS"]:
            filepath: str = os.path.join(self.__mods_folder_path, file)
            shutil.move(filepath, old_mods_folder)
            logging.info(f"Secured {filepath} in {old_mods_folder}")


    def _download_modpack(self):
        """
        Downloads the modpack and shows a progress bar for the progress.
        :return:
        """
        # TODO: ADD THE LOGIC FOR THE DOWNLOAD


    @staticmethod
    def __get_redirect_link() -> str:
        """
        Returns the redirection link from the GitHub "gist" to download the mods from.
        :return:
        """

        data: requests.Response = requests.get("https://gist.github.com/MrKelpy/6443d414004b00a583ab80b8e9187e65")
        soup: BeautifulSoup = BeautifulSoup(data.text, "html.parser")

        return soup.find(id="file-txt-LC1").text
