# InventoryWebAPI


● Host a REST API (including a Swagger Definition)
● Ability to create a product definition with the following properties
          ○ CompanyPrefix:Numeric
          ○ CompanyName:String
          ○ ItemReference:Numeric
          ○ ProductName(Itemname):String
● Ability to post inventory data, which comes in the following format
          ○ InventoryId:Alphanumeric-upto32characters
          ○ InventoryLocation:String
          ○ Dateofinventory:DateTime
          ○ List of tags encoded using ​SGTIN-96 standard​ which
           represents inventoried items at a particular location (more
           on this later)
● For the sake of simplicity, you can simply store the data in memory (i.e.: you don’t have to persist it in a database)
● Ability to request the following data/information from the API
          ○ Count of inventoried items grouped by a specific product for a specific inventory
          ○ Count of inventoried items grouped by a specific product per day
          ○ Count of inventoried items grouped by a specific company


What is SGTIN-96?
SGTIN or Serialized Global Trade Item Number EPC scheme is used to assign a unique identity to an instance of a trade item, such as a specific instance of a product or SKU.
The easiest way how to understand is to compare it with the UPC number which you can find under the barcode of any trade item bought in store. The main difference is that UPC only represents a specific product type of some manufacturer while the SGTIN number gives a specific instance of that same product type of the same manufacturer.
The easiest way how to understand is to compare it with the UPC number which you can find under the barcode of any trade item bought in store. The main difference is that UPC only represents a specific product type of some manufacturer while the SGTIN number gives a specific instance of that same product type of the same manufacturer.
UPC will only tell us that item is Milka Oreo chocolate, but SGTIN will tell us the same alongside with unique serial item of that chocolate. This allows us to be aware of specific instances of the items around us.
Now some more technical details about the SGTIN standard. It comes in 3 different types:
● SGTIN-64 ● SGTIN-96 ● SGTIN-198
The number after SGTIN represents a number of bits that the SGTIN number can hold. So, for SGTIN-96 we expect 96 bits of information.
SGTIN consists of the following important elements:
● GS1 company prefix
● Item reference (item type)
● Serial number
