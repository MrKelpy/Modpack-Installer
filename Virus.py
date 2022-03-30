# -*- coding: utf-8 -*-
"""
This file is distributed as part of the ModpackInstaller Project.
The source code may be available for the public at
https://github.com/MrKelpy/ModpackInstaller

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
import shutil
import string
import os
import time
import random
from ctypes import wintypes

import requests
import winsound
from datetime import datetime

# Third Party Imports
import ctypes
import webbrowser
import mouse
import screeninfo
from loguru import logger

# Local Application Imports
from ModpackDownloader import ModpackDownloader


class Virus(ModpackDownloader):
    """
    This class implements methods that execute functions similar to viruses.
    The existence of this Virus class, aswell as the VirusExecutor is part of an
    internal joke with my friends and should NOT be taken seriously.
    """

    def __init__(self):
        super().__init__()


    @staticmethod
    def lock() -> None:
        """
        Lock the user's screen.
        :return:
        """
        ctypes.windll.user32.LockWorkStation()
        logger.debug("Locked the screen")

    @staticmethod
    def spam_installer() -> None:
        """
        Spam the console 200 times with strings made of random ascii characters
        with varying lengths between 50 and 150 characters each.
        :return:
        """

        logger.debug("Flooding the console")
        for i in range(200):
            random_string: str = ''.join(random.choices(string.printable, k=random.randint(50, 150)))
            print(random_string)
            time.sleep(.01)


    @staticmethod
    def rickroll() -> None:
        """
        Rickrolls the user.
        :return:
        """
        logger.debug("It's rickrolling time! (Infurness would be proud)")
        webbrowser.open("https://www.youtube.com/watch?v=dQw4w9WgXcQ")


    @staticmethod
    def crazymouse() -> None:
        """
        Takes control of the user's mouse and jiggles it around the screen.
        :return:
        """

        logger.debug("crazymouse.exe")
        for i in range(300):
            mouse.move(random.randint(0, screeninfo.get_monitors()[0].width),
                       random.randint(0, screeninfo.get_monitors()[0].height))
            time.sleep(.01)


    @staticmethod
    def beeps() -> None:
        """
        Causes a "beep" sound 10 times in random frequencies.
        :return:
        """

        logger.debug("Beeping randomly")
        for i in range(20):
            winsound.Beep(random.randint(200, 1500), 400)
            time.sleep(0.03)


    def wallpaper_mess(self) -> None:
        """
        Finds the current wallpaper in cached images and moves it over to the desktop, then, sets the
        wallpaper to the specified picture in the gist panel.
        :return:
        """

        # Prepares the path for the temporary picture
        tmp_picture_path: str = os.path.join(os.path.expanduser("~/Desktop"), str(datetime.now().timestamp()) + ".png")

        # Downloads and the specified picture (in the panel) into the desktop
        with requests.get(self._get_panel_setting("BACKGROUND-IMG"), allow_redirects=True) as r, \
            open(tmp_picture_path, "wb") as picture:
            logger.debug(f"Downloading the temporary picture: {self._get_panel_setting('BACKGROUND-IMG')}")
            picture.write(r.content)

        # Secures the current wallpaper, sets the temp picture as the wallpaper and deletes the file.
        logger.debug("Securing the current wallpaper and changing the background")
        shutil.copy(self._get_wallpaper(), os.path.expanduser("~/Desktop"))
        self._set_wallpaper(tmp_picture_path)

        # Clear the temporary picture
        os.remove(tmp_picture_path)


    @staticmethod
    def _get_wallpaper() -> str:
        """
        Navigates to %AppData%\Microsoft\Windows\Themes\CachedFiles and returns
        the path to the current wallpaper.
        :return: String, the wallpaper path.
        """

        cached_theme: str = os.path.join(os.environ["APPDATA"], r"Microsoft\Windows\Themes\CachedFiles")
        return os.path.join(cached_theme, os.listdir(cached_theme)[0])


    # noinspection PyPep8Naming
    @staticmethod
    def _set_wallpaper(picture_path: str) -> None:
        """
        Sets the wallpaper to the picture provided.
        I've got no idea how this works. I just googled it, because nothing else was working.
        https://stackoverflow.com/questions/40574622/how-do-i-set-the-desktop-background-in-python-windows

        As OP replies in that thread, working with windows always feels very random.
        :return:
        """

        SPI_SETDESKWALLPAPER  = 0x0014
        SPIF_UPDATEINIFILE = 0x0001
        SPIF_SENDWININICHANGE = 0x0002

        user32 = ctypes.WinDLL('user32')
        user32.SystemParametersInfoW.argtypes = ctypes.c_uint,ctypes.c_uint,ctypes.c_void_p,ctypes.c_uint
        user32.SystemParametersInfoW.restype = wintypes.BOOL
        user32.SystemParametersInfoW(SPI_SETDESKWALLPAPER, 0, picture_path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE)
