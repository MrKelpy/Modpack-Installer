"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

import os
import sys
import tarfile
# Built-in Imports
from typing import List, Union

# Third Party Imports
from loguru import logger


# Local Application Imports


class LogHandler:
    """
    This class implements a handler for the logging functions,
    with utils for archiving log files.
    """

    def __init__(self):
        logger.remove()
        self.__logs_path = "./logs"
        self.__log_format = "[{level}] [{time:DD/MM/YYYY HH:mm:ss}]: {message}"
        self.__latest_log_path = os.path.join("./logs", "latest.log")
        os.makedirs(self.__logs_path, exist_ok=True)
        logger.add(sys.stdout, format=self.__log_format, level="INFO", backtrace=True, diagnose=True)
        logger.add(self.__latest_log_path, format=self.__log_format, level="DEBUG", backtrace=True, diagnose=True)


    def pack_latest(self) -> None:
        """
        Archives the latest.log file using .tar.gz compression
        with the date of logging (As seen in the first line of the logging file)
        if there is a "latest.log" file.
        When all is done, clears the file.
        :return:
        """

        if os.path.exists(self.__latest_log_path):

            if not self._get_latest_log_date():
                return

            tar = tarfile.open(os.path.join(self.__logs_path, self._get_latest_log_date() + ".tar.gz"), "w:gz")
            tar.add(self.__latest_log_path, arcname="data.log")
            tar.close()

            open(self.__latest_log_path, "w").close()  # Clear the latest.log file


    def _get_latest_log_date(self) -> Union[None, str]:
        """
        Returns the "latest.log" logging date, present at the first line of the logging
        file.
        :return: String, the date of logging.
        """

        with open(self.__latest_log_path, "r") as file:
            raw_date: List[str] = file.readlines()

        if not raw_date:
            return

        return raw_date[0].split(":")[-1].strip()
