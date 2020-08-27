-- MySQL dump 10.13  Distrib 8.0.17, for Win64 (x86_64)
--
-- Host: localhost    Database: dayoff
-- ------------------------------------------------------
-- Server version	8.0.17

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
-- Table structure for table `companies`
--

DROP TABLE IF EXISTS `companies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companies` (
  `id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `thumbnail_path` varchar(500) DEFAULT NULL,
  `name` varchar(5000) DEFAULT NULL,
  `description` varchar(5000) DEFAULT NULL,
  `created_by` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `date_created` datetime DEFAULT NULL,
  `date_modified` datetime DEFAULT NULL,
  `is_deleted` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `companies_user_fk` (`created_by`),
  CONSTRAINT `companies_user_fk` FOREIGN KEY (`created_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companies`
--

LOCK TABLES `companies` WRITE;
/*!40000 ALTER TABLE `companies` DISABLE KEYS */;
INSERT INTO `companies` VALUES ('31c98cce-6747-4472-b400-1b0622be7964',NULL,'company2','restaurant','fefc371a-bf63-4000-a811-e0183d0f843f','2020-08-19 16:17:23',NULL,NULL),('e676362f-988e-43a4-9043-6d15af458273',NULL,'company',NULL,'fefc371a-bf63-4000-a811-e0183d0f843f','2020-08-10 16:16:15',NULL,NULL);
/*!40000 ALTER TABLE `companies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `enrolment`
--

DROP TABLE IF EXISTS `enrolment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enrolment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` char(36) NOT NULL,
  `company_id` char(36) NOT NULL,
  `is_active` tinyint(1) NOT NULL,
  `date_added` datetime(6) NOT NULL,
  `date_finished` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `enrolment_fk` (`company_id`),
  KEY `enrolment_fk_1` (`user_id`),
  CONSTRAINT `enrolment_fk` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`),
  CONSTRAINT `enrolment_fk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `enrolment`
--

LOCK TABLES `enrolment` WRITE;
/*!40000 ALTER TABLE `enrolment` DISABLE KEYS */;
INSERT INTO `enrolment` VALUES (37,'fefc371a-bf63-4000-a811-e0183d0f843f','e676362f-988e-43a4-9043-6d15af458273',1,'2020-08-10 16:16:15.000000',NULL),(38,'fefc371a-bf63-4000-a811-e0183d0f843f','31c98cce-6747-4472-b400-1b0622be7964',1,'2020-08-19 16:17:23.000000',NULL);
/*!40000 ALTER TABLE `enrolment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_titles`
--

DROP TABLE IF EXISTS `job_titles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `job_titles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `company_id` char(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `titles_fk` (`company_id`),
  CONSTRAINT `titles_fk` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_titles`
--

LOCK TABLES `job_titles` WRITE;
/*!40000 ALTER TABLE `job_titles` DISABLE KEYS */;
/*!40000 ALTER TABLE `job_titles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `positions`
--

DROP TABLE IF EXISTS `positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `positions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `company_id` char(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `parts_fk` (`company_id`),
  CONSTRAINT `parts_fk` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `positions`
--

LOCK TABLES `positions` WRITE;
/*!40000 ALTER TABLE `positions` DISABLE KEYS */;
/*!40000 ALTER TABLE `positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` char(36) NOT NULL,
  `company_id` char(36) NOT NULL,
  `date` date NOT NULL,
  `start` time(6) NOT NULL,
  `end` time(6) NOT NULL,
  `date_added` datetime(6) NOT NULL,
  `date_finished` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `schedule_fk_1` (`user_id`),
  KEY `schedule_fk_2` (`company_id`),
  CONSTRAINT `schedule_fk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `schedule_fk_2` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule`
--

LOCK TABLES `schedule` WRITE;
/*!40000 ALTER TABLE `schedule` DISABLE KEYS */;
/*!40000 ALTER TABLE `schedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` char(36) NOT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password_hash` varchar(100) NOT NULL,
  `password_salt` varchar(200) NOT NULL,
  `first_name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `last_name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `is_admin` tinyint(1) DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT NULL,
  `date_activated` datetime(6) DEFAULT NULL,
  `date_added` datetime(6) DEFAULT NULL,
  `date_changed` datetime(6) DEFAULT NULL,
  `last_login` datetime(6) DEFAULT NULL,
  `title_id` int(11) DEFAULT NULL,
  `position_id` int(11) DEFAULT NULL,
  `valid_key` varchar(200) DEFAULT NULL,
  `is_deleted` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `fk_users_titles` (`title_id`),
  KEY `fk_users_positions` (`position_id`),
  CONSTRAINT `fk_users_positions` FOREIGN KEY (`position_id`) REFERENCES `positions` (`id`),
  CONSTRAINT `fk_users_titles` FOREIGN KEY (`title_id`) REFERENCES `job_titles` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('fefc371a-bf63-4000-a811-e0183d0f843f','qoh1234@gmail.com','7ZLW3fZ5k4xCLFXVjVEiL3quRNh5nCa/3klZW3SN2zL5jnlGG43pEqMshsK/lLioKyWXuMaMUgencIYrLz1Shg==','ZmaV4Eaw9hePUgxSyknncppFseaL7exKP9Y5QVxScEaW5KYpE/+kXzZrhjAPXzrNnLMejCr3dugdE5clUxqf1Jreunb/P5s0KJTzSU+xQSimNG3JGmPeLbDNqGsAvy7fvqidS21DoAtXLRk3scjko1FZt41e/DqWq2BtUkwxSY8=','name','123',1,1,'2020-08-10 16:16:15.000000','2020-08-10 16:16:15.000000','2020-08-11 19:44:34.000000','2020-08-19 16:16:24.000000',NULL,NULL,'4d212579-457f-41e4-8d20-468198cd1996',NULL);
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

-- Dump completed on 2020-08-27 15:00:54
