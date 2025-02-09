DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL,
  `Name` varchar(50) NOT NULL AUTO_INCREMENT,
  `LastName` varchar(50) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Country` varchar(45) NOT NULL,
  `Age` int DEFAULT NULL,
  `PhotoUrl` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

DELIMITER //
CREATE PROCEDURE Sp_GetUsers()
BEGIN
    SELECT Id, Name, LastName, Email, Country, Age, PhotoUrl FROM users;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE Sp_GetUserByEmail(
IN p_Email VARCHAR(45),
OUT p_Exists INT
)
BEGIN
    SELECT COUNT(*) INTO p_Exists
    FROM users
    WHERE TRIM(LOWER(Email)) = TRIM(LOWER(p_Email));

    IF p_Exists = 0 THEN
        SET p_Exists = 0;
    ELSE
        SET p_Exists = 1;
    END IF;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE Sp_AddUser(
	/*IN p_Id CHAR(16),*/
    IN p_Name VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_Email VARCHAR(45),
    IN p_Country VARCHAR(45),
    IN p_Age INT,
    IN p_PhotoUrl VARCHAR(500)
)
BEGIN
    INSERT INTO users (Name, LastName, Email, Country, Age, PhotoUrl) 
    VALUES (p_Name, p_LastName,  p_Email, p_Country, p_Age, p_PhotoUrl);
END //
DELIMITER ;