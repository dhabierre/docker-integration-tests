CREATE TABLE "Sample"
(
  "Id" SERIAL PRIMARY KEY NOT NULL,
  "IsCritical" BOOLEAN NOT NULL,
  "Message" VARCHAR(200) NOT NULL,
  "Timestamp" TIMESTAMP NOT NULL
);