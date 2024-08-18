# ClipTeleporter

Share your Clipboard over the internet.
Not only text and images, but also complex content like Office objects.

![Screenshot](/Doc/Images/ClipTeleporter.png?raw=true "Screenshot")

## Features
* End-to-end encryption

## Todo
* Settings
  * Launch on Windows startup
  * Server URL
  * Define hotkeys
* Export / import
* Filter

## Description
### Send current clipboard content to server:
[Strg] + [Alt] + [v] or "Send" button in client.

A notify message will appear and the token will be copied to the clipboard.
Also this clip will be added to the clip-list in the client.

### Get current clipboard content to server:
[Strg] + [Alt] + [c] or "Receive" button in client.

Before you can use the hotkey a valid token must be in the clipboard.
The receive button will get the selected clip of the clip-list.

A notify message will appear and the content will be copied to the clipboard.

### Description:
You can add a description to each clip in the clip-list.

### Clip-List:
All sent or received clips will be stored to a list in the client.
Duplicates (identic token) will not be stored.

### Token:
The token is split into two parts: Token#Password

"Token" is to identify a clip in the database on the server.

"Password" is to encrypt / decrypt the content. It will not be send to the server.


### Server:
The server is written in Python / Flask.
It provides a database and an API for access.
Also it shows a little info page:
https://clipteleporter.visio-shapes.com/