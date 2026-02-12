using UnityEngine;
using Ink;
using Ink.Runtime;
using System.Collections.Generic;
using System.IO;

public class DialogueVariables
    {

    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(string globalsFilePath)
    {
        // compile the story
        string inkFileContents = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();


        // intialise the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Intialised global dialogue Variable: " +  name + " = " + value);

        }
    }
    
    public void StartListening (Story story)
    {
        //variablesToStory must happen before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening (Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }





    public void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable Changed: " + name + " = " + value);

        //only maintain initialised variables from the global ink file
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);

        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}

