class persona:
 def __init_(self, nombre, edad):
  self.nombre = nombre
  self.edad= edad

 def saludar (self):
     return f"hola, soy {self.nombre}, {self.edad}"

p = persona ("sergio", 50)
print(p.saludar())
