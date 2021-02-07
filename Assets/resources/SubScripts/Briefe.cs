using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefe : MonoBehaviour
{
    private string[] brief1 = {@"\tLieber u...-.-r",
    @"\tEs ist nun schon eine Weile her, seitdem du uns",
    @"\tverlassen hast. Tro-z großer Mühe haben wir es leider",
    @"\tnicht geschaft dich zu finden. Während ich und dein",
    @"\tVater viel Zeit damit verbringen deinen Geschwistern",
    @"\tzu helfen, geht es in der Welt heiß umher. Eine Krankheit",
    @"\tscheint die Menschen zu befallen. Alchi hat sich vor 2",
    @"\tWochen aufgebracht ein Heilung zu finden, nur hat noch",
    @"\tniemand von ihm Gehört. Währenddessen bekämpfen sich die",
    @"\tMenschen im Dorf mitlerweile gegenseitig. Vorräte werden",
    @"\taus den Händen der Kranken gerissen und an die Adligen",
    @"\tverteilt. Aber um uns musst du dir zum Glück keine Sorgen",
    @"\tmachen. Vater hat die diesjährige Ernte zum großen Teil",
    @"\tversteckt, sodass wir eine gute Weile damit auskommen.",
    @"\tIch hoffe trotzdem, dass du bald wiederkommst.",
    @"\tLiebe Grüße",
    @"\t---.--.-.-- (Unlesbar)",
    @"\t(Seite 1/2)"};

    private string[] brief2 = {@"\tLieber -se-.-r\n",
    @"\tEs ist nun 5 Wochen her seitdem ich den ersten Brief ",
    @"\tgeschrieben habe. Ein paar Wanderer sind auf dem Weg ",
    @"\tzum Dorf an uns vorbeigekommen. Wir fragten sie, ob es",
    @"\tin anderen Teilen des Landes besser sei. Die Wanderer ",
    @"\tschauten sich nur gegenseitig an und schüttelten den ",
    @"\tKopf. Mittlerweile haben auch Vater Bedenken um uns. ",
    @"\tAus Sorge, das unsere Vorräte nicht reichen essen Mama",
    @"\tund Papa nicht einmal mehr täglich. 'Ihr seit wichtiger'",
    @"\tbekommen wir die ganze Zeit zu hören 'Alles wird gut'.\n\n",
    @"\t(Der rest des Briefes scheint nichtwirklich lesbar zu sein. ",
    @"\tNeben ein paar vereinzelten Buchstaben erkennst du nicht ",
    @"\tviel. Das einzige was du klar erkennen kannst ist Dick ",
    @"\tgeschriebenes 'USR' und eine Baum änliche Struktur, welche ",
    @"\tjemand auf den unteren Teil des Briefes gezeichnet hat)\n\n",
    @"\t(Seite 2/2)"};
    public string[] GetBrief1()
    {
        return brief1;
    }
    public string[] GetBrief2()
    {
        return brief2;
    }
}
