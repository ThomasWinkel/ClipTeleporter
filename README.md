# ClipTeleporter
Share your clipboard over the internet.
Not only text and images, but also complex content like Office objects.

![Screenshot](/Doc/Images/ClipTeleporter.png?raw=true "Screenshot")

## Workflow
The client application starts silent in the system tray.
It can be used without the GUI only by hotkeys:

<kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>v</kbd> will transfer the current clipboard content to the server.

In return an unique token (example: G3cAX8RULb#VK3GscXbY1) will be copied to your clipboard.

With this token, anyone can retrieve the content from the server to paste it somewhere. So you can send it by email or chat message to friends or colleagues.

First copy the token to your clipboard, then press <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>c</kbd>. Now the content is in your clipbord.

All this can also be done with the GUI. There you will find all sent and recaived clips in a list.

## End-to-end encryption
Clips will be compressed and encrypted before they are sent to the server.
So, only you and anyone who has the token are able to see the content.

The token is split into two parts: Token#Password

"Token" is to identify a clip in the database on the server.

"Password" is to encrypt / decrypt the content. It will not be send to the server.

## Todo
* Settings
  * Launch on Windows startup
  * Server URL
  * Define hotkeys
* Store clips local to have them available offline
* Export / import
* Search & Filter to find clips quick
* Special clips may require specific implementation...?
* Your ideas...

## Server:
The server is written in Python / Flask / SQLAlchemy.

It provides a database and an API for access.
Also it shows a little info page:

https://clipteleporter.visio-shapes.com/