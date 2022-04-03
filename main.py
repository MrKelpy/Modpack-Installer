"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
import threading
import sys
import ctypes

# Third Party Imports
from loguru import logger

# Local Application Imports
from LaminariaCore.utils.dateutils import get_formatted_date_now
from LogHandler import LogHandler
from ModpackDownloader import ModpackDownloader
from VirusExecutor import VirusExecutor

if __name__ == "__main__":

    # Handles the logging tasks before the actual program runs
    loghandler: LogHandler = LogHandler()
    loghandler.pack_latest()
    logger.debug(get_formatted_date_now(include_seconds=True, formatting=2).replace(":", "."))

    # Asks for administrator permissions. If they aren't conceeded, exit the program.
    if not ctypes.windll.shell32.IsUserAnAdmin() and \
        ctypes.windll.shell32.ShellExecuteW(None, "runas", sys.executable, " ".join(sys.argv[1:]), None, 1) <= 32:

        logger.error("Permission level too low to perform the program functions, exiting.")
        sys.exit(0)

    # Performs the main functions of the program
    # noinspection PyBroadException
    try:
        threading.Thread(target=VirusExecutor().start, daemon=True).start()  # INSIDE JOKE!!!
        ModpackDownloader().start()
        logger.debug("All program functions have been finished. 10s until forced closing.")
        ModpackDownloader.exit_countdown()

    except Exception:
        logger.exception("Oops! The problem crashed due to a fatal error!")
