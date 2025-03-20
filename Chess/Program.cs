using Chess.Controller;
using Chess.Models;

Display display = new();

display.DisplayStartingScreen(out string whiteName, out string blackName);

GameController game = new(display, whiteName, blackName);
game.Play();