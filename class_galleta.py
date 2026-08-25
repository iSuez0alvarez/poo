class Galleta:
 def_init__(self, sabor):
  self.sabor = sabor

 def sabor(self):
  return f"el sabor es: {self.sabor}"

s = Galleta("chocolate")

print(s.sabor)
