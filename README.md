The Treachery of Whales
=======================

# Om processen

Jag kände till Advent of code innan, men eftersom jag inte var tillräckligt insatt visste jag inte att
jag fick tillgång till exempeldata när jag loggade in. Men efter att jag upptäckte det så skrev jag
om programmet för att använda den datan istället för inmatning via tangentbordet (se tidigare commits
för den implementationen). Dessutom upptäckte jag vid inloggning att det fanns en del 2.

Jag har matat in mitt svar i advent of code så jag vet att mitt program räknar rätt med detta data set. Möjligtvis
har data setet ändrats sedan originalet eftersom mina uträckningar inte alltid stämmer med andras (vilket jag
upptäckte efter slutförd uppgift).

# Del 1 och del 2

För att ändra till svaret för del 2, ändra deligat i Main-metoden.

# Kommandoradsargument

`--verbose` Visar debugdata (i stil med den som visas i uppgiftsbeskrivningen).

# Om implementationen

Utöver utmaningen i Advent of code var uppgiften enligt e-post att testa:

  - Inläsning av data och formatering
  - Hantering av data (förhoppningsvis använder du dig utav en klass och funktioner)
  - Multi dimensionell data hantering

Därför har jag försökt följa detta genom att bara ha en klass och några metoder. Möjligtvis är följande
något överdrivet för ett program med denna storlek, men för ett större projekt skulle inmatning, utmatning,
beräkning och så vidare ligga i olika klasser.

I och med att detta också testade "Multi dimensionell data hantering" så valde jag en implementation som 
samlade alla positionsändringarna varje krabba behövde göra för varje position i en stor multi dimensionell
array. Min implementation tar upp mycket minne om det finns stora skillnader mellan de olika positionerna
och ett stort antal krabbor. Bör finnas bättre implementationer för mattegenier.
