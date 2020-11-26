-- Base de données APC
-- Jacquet Virgile et Matrod Rémi
-- MySQL v5.7

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `competence`
--

DROP TABLE IF EXISTS `competence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `competence` (
  `competence_id` int(11) NOT NULL AUTO_INCREMENT,
  `cours_id` int(11) NOT NULL,
  `competence_name` varchar(45) NOT NULL,
  `competence_description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`competence_id`),
  UNIQUE KEY `competence_id_UNIQUE` (`competence_id`),
  KEY `cours_id_idx` (`cours_id`),
  CONSTRAINT `fk_cours_id` FOREIGN KEY (`cours_id`) REFERENCES `cours` (`cours_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `competence`
--

LOCK TABLES `competence` WRITE;
/*!40000 ALTER TABLE `competence` DISABLE KEYS */;
/*!40000 ALTER TABLE `competence` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `controleur`
--

DROP TABLE IF EXISTS `controleur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `controleur` (
  `controleur_id` int(11) NOT NULL,
  `etudiant_id` int(11) NOT NULL,
  PRIMARY KEY (`controleur_id`,`etudiant_id`),
  KEY `etudiant_id_idx` (`etudiant_id`),
  CONSTRAINT `fk_controleur_id` FOREIGN KEY (`controleur_id`) REFERENCES `user` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_etudiant_id` FOREIGN KEY (`etudiant_id`) REFERENCES `user` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `controleur`
--

LOCK TABLES `controleur` WRITE;
/*!40000 ALTER TABLE `controleur` DISABLE KEYS */;
/*!40000 ALTER TABLE `controleur` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cours`
--

DROP TABLE IF EXISTS `cours`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cours` (
  `cours_id` int(11) NOT NULL AUTO_INCREMENT,
  `matiere_id` int(11) NOT NULL,
  `cours_name` varchar(45) NOT NULL,
  `cours_description` varchar(100) DEFAULT NULL,
  `cours_proprietaire` int(11) NOT NULL,
  PRIMARY KEY (`cours_id`),
  UNIQUE KEY `idcours_UNIQUE` (`cours_id`),
  KEY `matiere_id_idx` (`matiere_id`),
  KEY `cours_proprietaire_idx` (`cours_proprietaire`),
  CONSTRAINT `fk_cours_proprietaire` FOREIGN KEY (`cours_proprietaire`) REFERENCES `user` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_matiere_id` FOREIGN KEY (`matiere_id`) REFERENCES `matiere` (`matiere_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cours`
--

LOCK TABLES `cours` WRITE;
/*!40000 ALTER TABLE `cours` DISABLE KEYS */;
/*!40000 ALTER TABLE `cours` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `matiere`
--

DROP TABLE IF EXISTS `matiere`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `matiere` (
  `matiere_id` int(11) NOT NULL AUTO_INCREMENT,
  `matiere_name` varchar(45) NOT NULL,
  PRIMARY KEY (`matiere_id`),
  UNIQUE KEY `idmatiere_UNIQUE` (`matiere_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `matiere`
--

LOCK TABLES `matiere` WRITE;
/*!40000 ALTER TABLE `matiere` DISABLE KEYS */;
/*!40000 ALTER TABLE `matiere` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `succes`
--

DROP TABLE IF EXISTS `succes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `succes` (
  `succes_id` int(11) NOT NULL AUTO_INCREMENT,
  `succes_nom` varchar(45) NOT NULL,
  `succes_description` varchar(100) NOT NULL,
  `succes_valeur` int(11) NOT NULL DEFAULT '1',
  `succes_idprec` int(11) DEFAULT NULL,
  PRIMARY KEY (`succes_id`),
  UNIQUE KEY `succes_id_UNIQUE` (`succes_id`),
  KEY `succes_idprec_idx` (`succes_idprec`),
  CONSTRAINT `fk_succes_idprec` FOREIGN KEY (`succes_idprec`) REFERENCES `succes` (`succes_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `succes`
--

LOCK TABLES `succes` WRITE;
/*!40000 ALTER TABLE `succes` DISABLE KEYS */;
/*!40000 ALTER TABLE `succes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(20) NOT NULL,
  `user_passhash` varchar(32) NOT NULL,
  `user_type` varchar(10) NOT NULL DEFAULT 'etudiant',
  `user_createtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `user_fullname` varchar(50) NOT NULL,
  `user_xp` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `userid_UNIQUE` (`user_id`),
  UNIQUE KEY `username_UNIQUE` (`user_name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `validation_comp`
--

DROP TABLE IF EXISTS `validation_comp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `validation_comp` (
  `user_id` int(11) NOT NULL,
  `competence_id` int(11) NOT NULL,
  `validation_type` int(11) NOT NULL,
  `validation_ts` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`,`competence_id`),
  KEY `competence_id_idx` (`competence_id`),
  CONSTRAINT `fk_competence_id` FOREIGN KEY (`competence_id`) REFERENCES `competence` (`competence_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `validation_comp`
--

LOCK TABLES `validation_comp` WRITE;
/*!40000 ALTER TABLE `validation_comp` DISABLE KEYS */;
/*!40000 ALTER TABLE `validation_comp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `validation_succes`
--

DROP TABLE IF EXISTS `validation_succes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `validation_succes` (
  `succes_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `validation_ts` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`succes_id`,`user_id`),
  KEY `user_id_idx` (`user_id`),
  CONSTRAINT `fk_succes_id` FOREIGN KEY (`succes_id`) REFERENCES `succes` (`succes_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_id2` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `validation_succes`
--

LOCK TABLES `validation_succes` WRITE;
/*!40000 ALTER TABLE `validation_succes` DISABLE KEYS */;
/*!40000 ALTER TABLE `validation_succes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database '3659302_apc'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
