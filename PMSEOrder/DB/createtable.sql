﻿create table localorder(
       ID integer primary key,
       GUIIDID guid ,
       CustomerName text,
       PO text,
       Composition text,
       CompositionDetail text,
       ProductType text,
       Purity text,
       Quantity real,
       QuantityUnit  text,
       Dimension  text,
       DimensionDetails  text,
       Drawing  text,
       SampleNeed  text,
       SampleNeedRemark text,
       SampleForAnlysis text,
       SampleForAnlysisRemark text,   
       DeadLine  datetime,
       MinimumAcceptDefect text,
       ShipTo text,
       WithBackingPlate  text,
       PlateDrawing  text,
       SpecialRequirement text,
       BondingRequirement  text,
       PartNumber  text,  
       Remark  text,
       CreateTime DateTime,
       OrderState text
          
)