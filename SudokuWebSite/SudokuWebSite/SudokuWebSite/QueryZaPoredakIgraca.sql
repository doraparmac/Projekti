SELECT G.TournamentID, U.PlayerID, U.FirstName, U.LastName, G.TotalPoint
FROM (SELECT TournamentID, PlayerID, SUM(Point) AS TotalPoint
	FROM (
		SELECT MatchID, TournamentID, PlayerOneID AS PlayerID, PlayerOnePoint AS Point FROM Matches
		
			UNION

		SELECT MatchID, TournamentID, PlayerTwoID AS PlayerID, PlayerTwoPoint AS Point FROM Matches AS Matches_1
	      ) AS Un
		
		GROUP BY TournamentID, PlayerID
     ) AS G INNER JOIN Players AS U ON U.PlayerID = G.PlayerID