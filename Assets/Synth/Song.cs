using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Theme
{
    Main,
    Calm,
    Battle
}

[RequireComponent(typeof(Synth))]
public class Song : MonoBehaviour
{
    public Scale currentScale;
    public Drumkit drumkit;
    public Synth synth;
    public Tempo tempo;
    public int bassInstrument;
    public int[] rhythmInstruments;
    public int[] leadInstruments;
    public int seed;
    public Theme CurrentTheme
    {
        get 
        { 
            return this.currentTheme; 
        }
        set 
        { 
            this.currentTheme = value;
            this.SetTransition(Transition.End);
        }
    }

    private FSMSystem stateMachine;
    private Theme currentTheme;

    private Intro intro;
    private Chorus chorus;
    private Verse verse;
    private Bridge pre;
    private Bridge post;
    private Bridge ending;


    private Pool<Playable> chords;
    private Pool<Playable> notes;

    private const int MAXSIZE_CHORDS = 20;
    private const int MAXSIZE_NOTES = 50;
    private const int MIN_NOTE = 60;
    private const int MAX_NOTE = 72;

    public void Start()
    {
        this.synth = GetComponent<Synth>();
        Random.seed = this.seed;
        this.currentScale = new Scale();
        this.currentScale.fundamentalNote = Random.Range(MIN_NOTE, MAX_NOTE);
        this.tempo = new Tempo();
        this.stateMachine = new FSMSystem();

        this.chords = new Pool<Playable>(MAXSIZE_CHORDS, () => new Chord());
        this.notes = new Pool<Playable>(MAXSIZE_NOTES, () => new Note());

        this.intro = new Intro(this);
        this.verse = new Verse(this);
        this.chorus = new Chorus(this);
        this.pre = new Bridge(StateID.Pre, this);
        this.post = new Bridge(StateID.Post, this);
        this.ending = new Bridge(StateID.Ending, this);

        this.intro.AddTransition(Transition.Bridge, StateID.Pre).
            AddTransition(Transition.BeginVerse, StateID.Verse).
            AddTransition(Transition.End, StateID.Ending);                                                                  

        this.verse.AddTransition(Transition.Bridge, StateID.Post).
            AddTransition(Transition.BeginChorus, StateID.Chorus).
            AddTransition(Transition.End, StateID.Ending);

        this.chorus.AddTransition(Transition.Bridge, StateID.Pre).
            AddTransition(Transition.BeginVerse, StateID.Verse).
            AddTransition(Transition.End, StateID.Ending);

        this.pre.AddTransition(Transition.BeginVerse, StateID.Verse).
            AddTransition(Transition.End, StateID.Ending);

        this.post.AddTransition(Transition.BeginChorus, StateID.Chorus).
            AddTransition(Transition.End, StateID.Ending);

        this.ending.AddTransition(Transition.BeginIntro, StateID.Intro);

        intro.pianoInstrument = 1;
        verse.pianoInstrument = 1;

        stateMachine.AddState(intro);
        stateMachine.AddState(chorus);
        stateMachine.AddState(verse);
        stateMachine.AddState(pre);
        stateMachine.AddState(post);
        stateMachine.AddState(ending);
    }

    public void FixedUpdate()
    {
        stateMachine.CurrentState.Reason();
        stateMachine.CurrentState.Act();
    }

    public void OnDestroy()
    {
        notes.Dispose();
        chords.Dispose();
    }

    public void SetTransition(Transition transition)
    {
        stateMachine.PerformTransition(transition);
    }

    public Chord GetPooledChord()
    {
        return (Chord)chords.Acquire();
    }

    public Note GetPooledNote()
    {
        return (Note)notes.Acquire();
    }

    public void ReturnAllToPool(LinkedList<Playable> list)
    {
        foreach (Playable pooled in list)
        {
            switch (pooled.type)
            {
                case PlayableType.Undefined:
                    break;
                case PlayableType.Note:
                    notes.Release(pooled);
                    break;
                case PlayableType.Chord:
                    chords.Release(pooled);
                    break;
                default:
                    break;
            }
        }
    }
}
