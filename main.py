"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
import logging

# Third Party Imports
# Local Application Imports
from LaminariaCore.utils.dateutils import get_formatted_date_now
from LogHandler import LogHandler

if __name__ == "__main__":

    # Handles the logging tasks before the actual program runs
    loghandler: LogHandler = LogHandler()
    loghandler.pack_latest()
    logging.info(get_formatted_date_now(include_seconds=True, formatting=2).replace(":", "."))
