CREATE TABLE Countries (
	country_id BIGINT PRIMARY KEY IDENTITY(1,1) not null,
	title VARCHAR(45) NOT NULL
);
CREATE TABLE Players (
    player_id BIGINT PRIMARY KEY IDENTITY(1,1) not null,
	username VARCHAR(25) NOT NULL,
	email VARCHAR(256) NOT NULL,
	password VARCHAR(64) NOT NULL,
	first_name VARCHAR(45),
	last_name VARCHAR(45),
	country_id BIGINT,
	admin BIT NOT NULL,
	created_at DATETIME NOT NULL,
	last_login DATETIME,
	FOREIGN KEY (country_id) REFERENCES Countries(country_id)
);
CREATE TABLE Tournaments (
	tournament_id BIGINT PRIMARY KEY IDENTITY(1,1) not null,
	title VARCHAR(45) NOT NULL,
	start_date DATETIME NOT NULL,
	end_date DATETIME NOT NULL,
	country_id BIGINT,
	FOREIGN KEY (country_id) REFERENCES Countries(country_id)
);
CREATE TABLE Matches (
	match_id BIGINT PRIMARY KEY IDENTITY(1,1) not null,
	player_id BIGINT NOT NULL,
	tournament_id BIGINT NULL,
	time_played TIME NOT NULL,
	points INT NOT NULL,
	FOREIGN KEY (player_id) REFERENCES Players(player_id),
	FOREIGN KEY (tournament_id) REFERENCES Tournaments(tournament_id)
);

