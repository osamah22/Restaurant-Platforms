CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Restaurant" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Company" text,
    CONSTRAINT "PK_Restaurant" PRIMARY KEY ("Id")
);

CREATE TABLE "FoodItem" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "RestId" uuid NOT NULL,
    CONSTRAINT "PK_FoodItem" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FoodItem_Restaurant_RestId" FOREIGN KEY ("RestId") REFERENCES "Restaurant" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_FoodItem_RestId" ON "FoodItem" ("RestId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240825093821_Init', '8.0.8');

COMMIT;

START TRANSACTION;

ALTER TABLE "FoodItem" DROP CONSTRAINT "FK_FoodItem_Restaurant_RestId";

ALTER TABLE "Restaurant" DROP CONSTRAINT "PK_Restaurant";

ALTER TABLE "FoodItem" DROP CONSTRAINT "PK_FoodItem";

ALTER TABLE "Restaurant" RENAME TO restaurant;

ALTER TABLE "FoodItem" RENAME TO fooditem;

ALTER INDEX "IX_FoodItem_RestId" RENAME TO "IX_fooditem_RestId";

ALTER TABLE restaurant ADD CONSTRAINT "PK_restaurant" PRIMARY KEY ("Id");

ALTER TABLE fooditem ADD CONSTRAINT "PK_fooditem" PRIMARY KEY ("Id");

ALTER TABLE fooditem ADD CONSTRAINT "FK_fooditem_restaurant_RestId" FOREIGN KEY ("RestId") REFERENCES restaurant ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240825124617_ConvertedTableNamesToLowerCase', '8.0.8');

COMMIT;

