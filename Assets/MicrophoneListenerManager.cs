using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MicrophoneListenerManager : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    //public string commandSequence = "";
    public List<string> commandSequenceWords = new List<string>();

    private void Start()
    {
        CreateActionsDictionaryContent();

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += CommandRecognized;
        StartKeywordRecognizer();
    }

    private void CommandRecognized(PhraseRecognizedEventArgs speech)
    {
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void AddToCommandSequence(string word)
    {
        if (commandSequenceWords.Count < 1)
        {
            commandSequenceWords.Add(word);
        }
        else
        {
            commandSequenceWords.Add(word);
            RunTask(commandSequenceWords[0] + " " + commandSequenceWords[1]);
            commandSequenceWords.Clear();
        }
    }

    private void RunTask(string task)
    {
        Debug.Log("Running task: " + task);
    }


    public void StartKeywordRecognizer()
    {
        keywordRecognizer.Start();
    }
    public void StopKeywordRecognizer()
    {
        keywordRecognizer.Stop();
    }

    private void CreateActionsDictionaryContent()
    {
        //number voice comms
        actions.Add("one", One);
        actions.Add("two", Two);
        actions.Add("too", Two);
        actions.Add("to", Two);
        actions.Add("three", Three);
        actions.Add("tree", Three);
        actions.Add("four", Four);
        actions.Add("for", Four);
        actions.Add("five", Five);
        actions.Add("six", Six);
        actions.Add("seven", Seven);
        actions.Add("eight", Eight);
        actions.Add("ate", Eight);
        actions.Add("nine", Nine);
        actions.Add("zero", Zero);

        // Trolley voice comms
        actions.Add("alfa", Alfa);
        actions.Add("a", Alfa);
        actions.Add("aee", Alfa);
        actions.Add("aii", Alfa);
        actions.Add("eye", Alfa);
        actions.Add("eyy", Alfa);
        actions.Add("alpha", Alfa);
        actions.Add("bravo", Bravo);
        actions.Add("b", Bravo);
        actions.Add("bee", Bravo);
        actions.Add("charlie", Charlie);
        actions.Add("c", Charlie);
        actions.Add("see", Charlie);
        actions.Add("sii", Charlie);
        actions.Add("delta", Delta);
        actions.Add("d", Delta);
        actions.Add("dee", Delta);
        actions.Add("dii", Delta);

        //basic comms
        actions.Add("yes", Yes);
        actions.Add("no", No);


        // Double Trolley voice comms
        actions.Add("one alfa", OneAlfa);
        actions.Add("one bravo", OneBravo);
        actions.Add("one charlie", OneCharlie);
        actions.Add("one delta", OneDelta);

        actions.Add("two alfa", TwoAlfa);
        actions.Add("two bravo", TwoBravo);
        actions.Add("two charlie", TwoCharlie);
        actions.Add("two delta", TwoDelta);

        actions.Add("three alfa", ThreeAlfa);
        actions.Add("three bravo", ThreeBravo);
        actions.Add("three charlie", ThreeCharlie);
        actions.Add("three delta", ThreeDelta);

        actions.Add("four alfa", FourAlfa);
        actions.Add("four bravo", FourBravo);
        actions.Add("four charlie", FourCharlie);
        actions.Add("four delta", FourDelta);

        actions.Add("five alfa", FiveAlfa);
        actions.Add("five bravo", FiveBravo);
        actions.Add("five charlie", FiveCharlie);
        actions.Add("five delta", FiveDelta);

        actions.Add("six alfa", SixAlfa);
        actions.Add("six bravo", SixBravo);
        actions.Add("six charlie", SixCharlie);
        actions.Add("six delta", SixDelta);

        actions.Add("seven alfa", SevenAlfa);
        actions.Add("seven bravo", SevenBravo);
        actions.Add("seven charlie", SevenCharlie);
        actions.Add("seven delta", SevenDelta);

        actions.Add("eight alfa", EightAlfa);
        actions.Add("eight bravo", EightBravo);
        actions.Add("eight charlie", EightCharlie);
        actions.Add("eight delta", EightDelta);

        actions.Add("nine alfa", NineAlfa);
        actions.Add("nine bravo", NineBravo);
        actions.Add("nine charlie", NineCharlie);
        actions.Add("nine delta", NineDelta);

        actions.Add("zero alfa", ZeroAlfa);
        actions.Add("zero bravo", ZeroBravo);
        actions.Add("zero charlie", ZeroCharlie);
        actions.Add("zero delta", ZeroDelta);
    }

    //Trolley voice comms
    private void Alfa()
    {
        AddToCommandSequence("alfa");
    }
    private void Bravo()
    {
        AddToCommandSequence("bravo");
    }
    private void Charlie()
    {
        AddToCommandSequence("chralie");
    }
    private void Delta()
    {
        AddToCommandSequence("delta");
    }

    //Basic comms
    private void Yes()
    {
        RunTask("yes");
    }
    private void No()
    {
        RunTask("no");
    }

    //number functions
    private void One()
    {
        AddToCommandSequence("one");
    }
    private void Two()
    {
        AddToCommandSequence("two");
    }
    private void Three()
    {
        AddToCommandSequence("three");
    }
    private void Four()
    {
        AddToCommandSequence("four");
    }
    private void Five()
    {
        AddToCommandSequence("five");
    }
    private void Six()
    {
        AddToCommandSequence("six");
    }
    private void Seven()
    {
        AddToCommandSequence("seven");
    }
    private void Eight()
    {
        AddToCommandSequence("eight");
    }
    private void Nine()
    {
        AddToCommandSequence("nine");
    }
    private void Zero()
    {
        AddToCommandSequence("zero");
    }

    //Double comms
    private void OneAlfa()
    {
        AddToCommandSequence("one");
        AddToCommandSequence("alfa");
    }
    private void OneBravo()
    {
        AddToCommandSequence("one");
        AddToCommandSequence("bravo");
    }
    private void OneCharlie()
    {
        AddToCommandSequence("one");
        AddToCommandSequence("charlie");
    }
    private void OneDelta()
    {
        AddToCommandSequence("one");
        AddToCommandSequence("delta");
    }

    private void TwoAlfa()
    {
        AddToCommandSequence("two");
        AddToCommandSequence("alfa");
    }
    private void TwoBravo()
    {
        AddToCommandSequence("two");
        AddToCommandSequence("bravo");
    }
    private void TwoCharlie()
    {
        AddToCommandSequence("two");
        AddToCommandSequence("charlie");
    }
    private void TwoDelta()
    {
        AddToCommandSequence("two");
        AddToCommandSequence("delta");
    }

    private void ThreeAlfa()
    {
        AddToCommandSequence("three");
        AddToCommandSequence("alfa");
    }
    private void ThreeBravo()
    {
        AddToCommandSequence("three");
        AddToCommandSequence("bravo");
    }
    private void ThreeCharlie()
    {
        AddToCommandSequence("three");
        AddToCommandSequence("charlie");
    }
    private void ThreeDelta()
    {
        AddToCommandSequence("three");
        AddToCommandSequence("delta");
    }

    private void FourAlfa()
    {
        AddToCommandSequence("four");
        AddToCommandSequence("alfa");
    }
    private void FourBravo()
    {
        AddToCommandSequence("four");
        AddToCommandSequence("bravo");
    }
    private void FourCharlie()
    {
        AddToCommandSequence("four");
        AddToCommandSequence("charlie");
    }
    private void FourDelta()
    {
        AddToCommandSequence("four");
        AddToCommandSequence("delta");
    }

    private void FiveAlfa()
    {
        AddToCommandSequence("five");
        AddToCommandSequence("alfa");
    }
    private void FiveBravo()
    {
        AddToCommandSequence("five");
        AddToCommandSequence("bravo");
    }
    private void FiveCharlie()
    {
        AddToCommandSequence("five");
        AddToCommandSequence("charlie");
    }
    private void FiveDelta()
    {
        AddToCommandSequence("five");
        AddToCommandSequence("delta");
    }

    private void SixAlfa()
    {
        AddToCommandSequence("six");
        AddToCommandSequence("alfa");
    }
    private void SixBravo()
    {
        AddToCommandSequence("six");
        AddToCommandSequence("bravo");
    }
    private void SixCharlie()
    {
        AddToCommandSequence("six");
        AddToCommandSequence("charlie");
    }
    private void SixDelta()
    {
        AddToCommandSequence("six");
        AddToCommandSequence("delta");
    }

    private void SevenAlfa()
    {
        AddToCommandSequence("seven");
        AddToCommandSequence("alfa");
    }
    private void SevenBravo()
    {
        AddToCommandSequence("seven");
        AddToCommandSequence("bravo");
    }
    private void SevenCharlie()
    {
        AddToCommandSequence("seven");
        AddToCommandSequence("charlie");
    }
    private void SevenDelta()
    {
        AddToCommandSequence("seven");
        AddToCommandSequence("delta");
    }

    private void EightAlfa()
    {
        AddToCommandSequence("eight");
        AddToCommandSequence("alfa");
    }
    private void EightBravo()
    {
        AddToCommandSequence("eight");
        AddToCommandSequence("bravo");
    }
    private void EightCharlie()
    {
        AddToCommandSequence("eight");
        AddToCommandSequence("charlie");
    }
    private void EightDelta()
    {
        AddToCommandSequence("eight");
        AddToCommandSequence("delta");
    }

    private void NineAlfa()
    {
        AddToCommandSequence("nine");
        AddToCommandSequence("alfa");
    }
    private void NineBravo()
    {
        AddToCommandSequence("nine");
        AddToCommandSequence("bravo");
    }
    private void NineCharlie()
    {
        AddToCommandSequence("nine");
        AddToCommandSequence("charlie");
    }
    private void NineDelta()
    {
        AddToCommandSequence("nine");
        AddToCommandSequence("delta");
    }

    private void ZeroAlfa()
    {
        AddToCommandSequence("zero");
        AddToCommandSequence("alfa");
    }
    private void ZeroBravo()
    {
        AddToCommandSequence("zero");
        AddToCommandSequence("bravo");
    }
    private void ZeroCharlie()
    {
        AddToCommandSequence("zero");
        AddToCommandSequence("charlie");
    }
    private void ZeroDelta()
    {
        AddToCommandSequence("zero");
        AddToCommandSequence("delta");
    }
}
