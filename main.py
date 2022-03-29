"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack Installer 

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
import os
import logging
import tarfile

# Third Party Imports
# Local Application Imports
from globals import LATEST_LOG_PATH


def pack_latest() -> None:
    """
    Archives the latest.log file using .tar.gz compression
    with the date of logging (As seen in the first line of the logging file)
    if there is a latest.log file.

    :return:
    """

    if os.path.isfile(LATEST_LOG_PATH):

        logging_date: str =
        with tarfile.open()

if __name__ == "__main__":
    logging.basicConfig(format="[%(levelname)s]: %(message)s",
                        filename=)



