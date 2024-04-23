-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: main
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `applications`
--

DROP TABLE IF EXISTS `applications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applications` (
  `ApplicationID` int NOT NULL AUTO_INCREMENT,
  `JobID` int NOT NULL,
  `EmployeeId` int NOT NULL,
  `ApplicationDate` date NOT NULL,
  `ApplicationStatus` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`ApplicationID`),
  KEY `jobid_idx` (`JobID`),
  KEY `EmployeeId2_idx` (`EmployeeId`),
  CONSTRAINT `EmployeeId2` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`EmployeeID`),
  CONSTRAINT `jobid` FOREIGN KEY (`JobID`) REFERENCES `jobs` (`JobID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `applications`
--

LOCK TABLES `applications` WRITE;
/*!40000 ALTER TABLE `applications` DISABLE KEYS */;
INSERT INTO `applications` VALUES (1,12,7,'2024-04-19','string');
/*!40000 ALTER TABLE `applications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employees` (
  `EmployeeID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `ContactNumber` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Portfolio` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `Resume` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `First_Name` varchar(45) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Last_Name` varchar(45) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Picture` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `Language` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `HighestEducation` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `NameofSchool` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `PreviousWork` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `Introduction` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `Address` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Profession` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Position` varchar(45) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`EmployeeID`),
  KEY `UserId_idx` (`UserID`),
  CONSTRAINT `fk12` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (7,49,'string','string','string','string','string','string','string','string','string','string','string','string','string','string');
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employers`
--

DROP TABLE IF EXISTS `employers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employers` (
  `EmployerID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `CompanyName` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CompanyDescription` text CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CompanyLogo` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci NOT NULL,
  `CompanyLocation` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Size` int DEFAULT NULL,
  `enterprisetype` varchar(45) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`EmployerID`),
  KEY `fk3_idx` (`UserID`),
  KEY `fk4_idx` (`UserID`),
  CONSTRAINT `userid` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employers`
--

LOCK TABLES `employers` WRITE;
/*!40000 ALTER TABLE `employers` DISABLE KEYS */;
/*!40000 ALTER TABLE `employers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jobs`
--

DROP TABLE IF EXISTS `jobs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jobs` (
  `JobID` int NOT NULL AUTO_INCREMENT,
  `EmployerID` int DEFAULT NULL,
  `JobTitle` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `JobDescription` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `JobRequirements` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `JobLocation` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `MinSalary` text COLLATE utf8mb3_hungarian_ci,
  `Picture` text CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci,
  `Position` varchar(45) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `Short_Summary` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `jobscol` varchar(45) COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  `MaxSalary` text COLLATE utf8mb3_hungarian_ci,
  `Extras` text COLLATE utf8mb3_hungarian_ci,
  PRIMARY KEY (`JobID`),
  KEY `fk2_idx` (`EmployerID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jobs`
--

LOCK TABLES `jobs` WRITE;
/*!40000 ALTER TABLE `jobs` DISABLE KEYS */;
INSERT INTO `jobs` VALUES (12,NULL,'dasads',NULL,NULL,'vasd','2000',NULL,NULL,NULL,NULL,NULL,NULL),(13,NULL,'dasads','dsads',NULL,'vasd','2000',NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `jobs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'employer'),(2,'employee'),(3,'Admin'),(7,'string');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Password` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `RoleID` int NOT NULL DEFAULT '1',
  `Salt` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  KEY `RoleID` (`RoleID`),
  CONSTRAINT `fk` FOREIGN KEY (`RoleID`) REFERENCES `roles` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (49,'string','string','string',1,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-23 16:32:37
