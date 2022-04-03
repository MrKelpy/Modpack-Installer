# -*- coding: utf-8 -*-
"""
This file is distributed as part of the ModpackInstaller Project.
The source code may be available for the public at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
import webbrowser
from typing import Optional, List
import sys
import time
import os
import signal
import ctypes

# Third Party Imports
from loguru import logger
import requests
from bs4 import BeautifulSoup

# Local Application Imports


class GeneralUtils:
    """
    This class implements some useful methods that don't fit in any of the other
    classes in terms of responsabilities, so they end up here.
    """

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


    @staticmethod
    def ensure_admin_perms() -> int:
        """
        Asks for admin permissions. If the user doesn't accept them,
        exit the program.
        :return: int, the response code for the admin perms. If the user is an admin, the code is 256.
        """

        if ctypes.windll.shell32.IsUserAnAdmin():
            return 256

        else:
            return ctypes.windll.shell32.ShellExecuteW(None, "runas", sys.executable, " ".join(sys.argv[1:]), None, 1)


    @staticmethod
    def get_panel_setting(setting: str) -> Optional[str]:
        """
        Returns a value for a given setting in the github gist panel

        :param str setting: The setting to get the value from.
        :return str setting_value: The value for the specified setting
        """

        data: requests.Response = requests.get("https://gist.github.com/MrKelpy/6443d414004b00a583ab80b8e9187e65")
        soup: BeautifulSoup = BeautifulSoup(data.text, "html.parser")
        lines: List[str] = [y.text for y in soup.find_all(class_="blob-code blob-code-inner js-file-line")]
        setting_value: Optional[str] = [x.split(">>")[1].strip() for x in lines if x.startswith(setting)][0]

        return setting_value


    @staticmethod
    def check_for_update(current_version: str) -> bool:
        """
        Checks if the program is up-to-date, if not, asks the user if they
        want to update. If the response is "y", send them to the download link.

        :param str current_version: The current program version.
        :return: Bool, whether the user wants to update or not.
        """

        latest_version: str = GeneralUtils().get_panel_setting("LATEST-VERSION")
        download_link: str = f"https://github.com/MrKelpy/Modpack-Installer/releases/download/{latest_version}/Modpack.Installer-v{latest_version}.zip"

        if latest_version == current_version:
            return False

        logger.warning(f"This version of the Modpack Installer ({current_version}) is outdated. "
                       f"A new version ({latest_version}) is available. Update? (Y/N)")

        if input().lower() == "y":
            webbrowser.open(download_link)
            return True

        return False
