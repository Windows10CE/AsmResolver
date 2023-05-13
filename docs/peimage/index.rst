Overview
========

.. note:: 

    The documentation has a new home: `Check it out <https://docs.washi.dev/asmresolver>`_!


The PE image layer is the second layer of abstraction of the portable executable (PE) file format. It is mostly represented by ``IPEImage`` and its derivatives (such as ``PEImage``), and works on top of the ``PEFile`` layer. Its main purpose is to provide access to mutable models that are easier to use than the raw data structures the PE file layer exposes. 
