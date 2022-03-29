"""
This file is distributed as part of the Modpack Installer Project.
The source code may be available at
https://github.com/MrKelpy/Modpack-Installer

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
import os
import logging

# Third Party Imports
# Local Application Imports


class LogHandler:
    """
    This class implements a handler for the logging functions,
    with utils for archiving and writing into log files and more.
    """

    def __init__(self):
        self.__logs_path = "./logs"
        self.__log_format = r"[%(levelname)s]: %(message)s"
        self.__latest_log_path = os.path.join("./logs", "latest.log")
        logging.basicConfig(format=self.__log_format)