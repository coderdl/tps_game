import MySQLdb
from Config import *

class DBManagement():


    def __init__(self):
        self.users = [{"username": '"ne"', "password": '"123456"'}]
        self.games = []
        self.status = []

    def _new_game(self, playerId, gameId):
        return {"playerId": playerId, "gameId": gameId, "isOver": False}

    def _new_player(self, playerId, gameId):
        return {"playerId": playerId, "gameId": gameId, "position": None, "rotation": None,
                "projectle": 20, "blood": 10}

    def new_game_(self, playerId):
        gameId = len(self.status) + 1
        self.status.append(self._new_game(playerId, gameId))
        self.games.append(self._new_player(playerId, gameId))
        return str(gameId)

    def login_game_(self, username, password):
        for item in self.users:
            if item['username'] == username and item['password'] == password:
                return True
            else:
                return False

    def game_over_(self, gameId):
        for item in self.status:
            if item['gameId'] == gameId:
                item['isOver'] = True
                break

    def takeDamage(self, gameId, playerId):
        for item in self.games:
            if item['gameId'] == gameId and item['playerId'] == playerId:
                item['blood'] -= 1

    def fire(self, gameId, playerId):
        for item in self.games:
            if item['gameId'] == gameId and item['playerId'] == playerId:
                item['projectile'] -= 1

    def reloadProfile(self, gameId, playerId):
        for item in self.games:
            if item['gameId'] == gameId and item['playerId'] == playerId:
                item['projectile'] = 20

    def reloadGame(self, playerId):
        for item in self.status:
            if item['playerId'] == playerId and not item['isOver']:
                return None


    def updatePlayerStatus(self, gameId, playerId, position, rotation):
        for item in self.games:
            if item['gameId'] == gameId and item['playerId'] == playerId:
                item['position'] = position
                item['rotation'] = rotation

    def addBot(self, gameId, botId):
        self.games.append(self._new_player(botId, gameId))

    def canLoadGame(self, playerId):
        for item in self.status:
            if item['playerId'] == playerId and not item['isOver']:
                return True
        return False

    def reloadBot(self, gameId):
        results = []
        for item in self.games:
            if item['gameId'] == gameId and item['blood'] > 0 and item['playerId'][:3] == 'bot':
                results.append(item)
        return results

    def reloadPlayer(self, playerId):
        gameId = -1
        for item in self.status:
            if item['playerId'] == playerId and not item['isOver']:
                 gameId = item['gameId']
                 break

        for item in self.games:
            if item['gameId'] == gameId and item['playerId'] == playerId:
                return item



