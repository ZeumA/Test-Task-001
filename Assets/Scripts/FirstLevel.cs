using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.IO;
using Random = System.Random;
using UnityEngine.UI;

public class FirstLevel : MonoBehaviour
{
    public GameObject Answer;
    public GameObject animateObject;
    public GameObject Text;
    public GameObject StartButton;
    public GameObject Panel;
    public Sprite[] sprites;
    public string[] names;
    List<string> UsedAnswers;
    List<GameObject> objects = new List<GameObject>();
    int state = 0;
    public void Start()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        UsedAnswers = new List<string>();
    }

    public void Begin()
    {
        var seq = DOTween.Sequence();
        seq.Append(StartButton.transform.DOScale(new Vector3(0, 0, 1), 0.3f));
        seq.Append(StartButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f));
        seq.Append(StartButton.transform.DOScale(new Vector3(0.85f, 0.85f, 1), 0.3f));
        seq.Append(StartButton.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f));
        seq.Append(Panel.GetComponent<Image>().DOFade(0f, 0.05f));

        seq.OnComplete(NextLevel);
    }

    public void NextLevel()
    {
        randomList.Clear();

        LoadSprites();
        //Loading sprites


        if (state == 0)
        {
            Panel.SetActive(false);

            foreach (var item in objects)
            {
                Destroy(item);
            }

            objects.Clear();
            //Destroy preavious answers


            Button button =
            StartButton.GetComponent<Button>();

            StartButton.gameObject.
                SetActive(false);
            //Disable start button

            SetupFirst();
            state = 1;
        }
        else
        if (state == 1)
        {
            SetupSecond();
            state = 2;
        }
        else
        if (state == 2)
        {
            SetupThird();
            state = 3;
        }
        else
        if (state == 3)
        {
            Button button =
            StartButton.GetComponent<Button>();

            StartButton.gameObject.
                SetActive(true);
            Panel.SetActive(true);
            Panel.GetComponent<Image>().DOFade(0.5f, 1);
            foreach (var item in objects)
            {
                var temp = item.GetComponentInChildren<Button>();
                temp.interactable = false;
            }

            StartButton.GetComponent<Image>().sprite =
                Resources.LoadAll<Sprite>("Textures/UI_Buttons")[1];
            //Enable start button

            Text text =
                Text.GetComponent<Text>();

            text.DOFade(0, 1);

            state = 4;
        }
        else
        if (state == 4)
        {
            StartButton.SetActive(false);

            var seq = DOTween.Sequence();
            seq.Append(Panel.GetComponent<Image>().DOFade(1f, 1));
            seq.Append(Panel.GetComponent<Image>().DOFade(0f, 1).SetDelay(3).OnStart(() => 
            {
                foreach (var item in objects)
                {
                    Destroy(item);
                }

                objects.Clear();
                //Destroy preavious answers
                StartButton.GetComponent<Image>().sprite =
                Resources.LoadAll<Sprite>("Textures/UI_Buttons")[2];
            }));
            seq.OnComplete(() => 
            {
                StartButton.SetActive(true);
            });
            UsedAnswers.Clear();
            state = 0;
        }
    }

    public void SetupFirst()
    {
        foreach (var item in objects)
        {
            Destroy(item);
        }

        objects.Clear();
        //Destroy preavious answers

        int right =
            NewNumbers(3, sprites.Length);
        UsedAnswers.Add(names[randomList[right]]);
        //Set right answer


        Text text =
            Text.GetComponent<Text>();

        text.text =
            "Find " + names[randomList[right]];

        text.DOFade(1, 1);
        //Setup text task


        for (int i = 0; i < 3; i++)
        {
            GameObject answer =
                Instantiate(
                    Answer,
                    new Vector3(-140 + i * 140, 0),  // Set static position
                    new Quaternion()) as GameObject;
            objects.Add(answer);
            //Instantiate new Answer

            Image image =
                objects[i].GetComponentInChildren<Image>();

            BounceOnCondition script =
                objects[i].GetComponentInChildren<BounceOnCondition>();
            //Getting necessary components


            image.sprite =
                (Sprite)sprites[randomList[i]];

            script.isRight =
                right == i ? true : false;

            script.Complete +=
                NextLevel;
            //Setup components
        }
    }

    public void SetupSecond()
    {
        foreach (var item in objects)
        {
            Destroy(item);
        }

        objects.Clear();
        //Destroy preavious answers


        int right =
            NewNumbers(6, sprites.Length);

        while (UsedAnswers.Contains(names[randomList[right]]))
        {
            randomList.Clear();

            right =
                NewNumbers(6, sprites.Length);
        }
        UsedAnswers.Add(names[randomList[right]]);
        //Set right answer


        Text text =
            Text.GetComponent<Text>();

        text.text =
            "Find " + names[randomList[right]];

        text.DOFade(1, 1);
        //Setup text task


        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                GameObject answer =
                    Instantiate(
                        Answer,
                        new Vector3(-140 + i * 140, -70 + j * 140),  // Set static position
                        new Quaternion()) as GameObject;
                objects.Add(answer);
                //Instantiate new Answer

                Image image =
                    objects[i * 2 + j].GetComponentInChildren<Image>();

                BounceOnCondition script =
                    objects[i * 2 + j].GetComponentInChildren<BounceOnCondition>();
                //Getting necessary components


                image.sprite =
                    (Sprite)sprites[randomList[i * 2 + j]];

                script.isRight =
                    right == (i * 2 + j) ? true : false;

                script.Complete +=
                    NextLevel;
                //Setup components
            }
        }
    }

    public void SetupThird()
    {
        foreach (var item in objects)
        {
            Destroy(item);
        }

        objects.Clear();
        //Destroy preavious answers


        int right =
            NewNumbers(9, sprites.Length);

        while (UsedAnswers.Contains(names[randomList[right]]))
        {
            randomList.Clear();

            right =
                NewNumbers(9, sprites.Length);
        }
        UsedAnswers.Add(names[randomList[right]]);
        //Set right answer


        Text text =
            Text.GetComponent<Text>();

        text.text =
            "Find " + names[randomList[right]];

        text.DOFade(1, 1);
        //Setup text task


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject answer =
                    Instantiate(
                        Answer,
                        new Vector3(-140 + i * 140, -140 + j * 140),  // Set static position
                        new Quaternion()) as GameObject;
                objects.Add(answer);
                //Instantiate new Answer

                Image image =
                    objects[i * 3 + j].GetComponentInChildren<Image>();

                BounceOnCondition script =
                    objects[i * 3 + j].GetComponentInChildren<BounceOnCondition>();
                //Getting necessary components


                image.sprite =
                    (Sprite)sprites[randomList[i * 3 + j]];

                script.isRight =
                    right == (i * 3 + j) ? true : false;

                script.Complete +=
                    NextLevel;
                //Setup components
            }
        }
    }

    void LoadSprites()
    {
        DirectoryInfo dinfo =
            new DirectoryInfo(
                Application.dataPath + "/Resources/Names");
     FileInfo[] Files = 
            dinfo.GetFiles("*.txt");

        int rand = 
            new Random().Next(Files.Length);

        string fullText = 
            File.ReadAllText(Files[rand].FullName);

        string[] split = fullText.Split('@');

        char[]
            param = { '\n', '\r' };
        
        string[] sprites = split[0].Split(param, StringSplitOptions.RemoveEmptyEntries);

        string[] names = split[1].Split(param, StringSplitOptions.RemoveEmptyEntries);
        List<Sprite> list = new List<Sprite>();

        foreach (string sprite in sprites)
        {
            list.AddRange(
                Resources.LoadAll<Sprite>(
                    "Textures/" + sprite));
        }

        this.sprites = list.ToArray();
        this.names = names;
    }

    public Random a = new Random(DateTime.Now.Ticks.GetHashCode());
    public List<int> randomList = new List<int>();
    int MyNumber = 0;
    private int NewNumbers(int count, int max)
    {
        for (int i = 0; i < count; i++)
        {
            MyNumber = a.Next(0, max);
            while (randomList.Contains(MyNumber))
                MyNumber = a.Next(0, max);

            randomList.Add(MyNumber);
        }

        return a.Next(count);
    }

    public void bounceEffect(GameObject item)
    {
        var seq = DOTween.Sequence();
        var ch = item.transform.GetChild(1);
        seq.Append(ch.transform.DOScale(new Vector3(0, 0, 1), 0.3f));
        seq.Append(ch.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f));
        seq.Append(ch.transform.DOScale(new Vector3(0.95f, 0.95f, 1), 0.3f));
        seq.Append(ch.transform.DOScale(new Vector3(1, 1, 1), 0.3f));
    }
}
