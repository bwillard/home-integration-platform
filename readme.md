# Home Integration Platform

The goal of the Home Integration Platform is to be an open
platform for connecting devices in your home to each other
and the outside world.

As an example we want to let you:
 * Control your Z-Wave enabled lights from a web page
 * Get a text if there is water in your basement
 * Turn on the lights when a door opens


## Overall Design


The overall idea is to have 2 + N parts

 * A generic engine that wire power the interactions between things based on a specified configuration
 * A UI to view the configuration and do manual interactions between things
 * An extensible set of libraries that allow you to interact with things like Z-Wave devices, and web services

A built-in set of adapters will be included but the idea is that is should be pretty easy to add new adapters.
