# ORM_CORE
ORM Funcionality

Due to database first approach, every change to the underlying code for ORM may break due to alteration, so entitiy creation related functions are stored seperatly and accesibly  in a static way. Still need to see if this shit really wokrs :|

New approach is the usage of extension classes wich are capable to work like normal object methods, this can help readability and mainainability in the long run.

TODO:
  -See how EntitiFramework handles id and connections
  -Implement the usage of classes that store only constants, in this way we can guarante that every given  
   program id represent the same program
