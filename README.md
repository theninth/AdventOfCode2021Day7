The Treachery of Whales
=======================

# Om implementationen

Utöver utmaningen i Advent of code var uppgiften enligt e-post att testa:

  - Inläsning av data och formatering
  - Hantering av data (förhoppningsvis använder du dig utav en klass och funktioner)
  - Multi dimensionell data hantering

Därför har jag försökt följa detta genom att bara ha en klass och några metoder. Möjligtvis är följande
något överdrivet för ett program med denna storlek, men för ett större projekt skulle inmatning, utmatning,
beräkning och så vidare ligga i olika klasser.

Inläsning av data tolkade jag som att det rörde läsning av text från användarens tangentbord. En
annan möjlig tolkning är förstås läsning från fil.
 
I och med att detta också testade "Multi dimensionell data hantering" så valde jag en implementation som 
samlade alla positionsändringarna varje krabba behövde göra för varje position i en stor multi dimensionell
array. Min implementation tar upp mycket minne om det finns stora skillnader mellan de olika positionerna
och ett stort antal krabbor.
