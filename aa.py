# -*- coding: utf-8 -*-
"""
This file is distributed as part of the ModpackInstaller Project.
The source code may be available for the public at
https://github.com/MrKelpy/ModpackInstaller

If a license applies for this project, the former can be found
in every distribution, as a "LICENSE" file at top level.
"""

# Built-in Imports
# Third Party Imports
# Local Application Imports
import time
from alive_progress import alive_bar

with alive_bar(100, force_tty=True, title="Downloading mods", elapsed=False, stats=False) as bar:

    for i in range(100):
        time.sleep(0.1)
        bar()