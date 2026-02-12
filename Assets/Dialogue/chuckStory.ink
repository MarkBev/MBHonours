INCLUDE globals.ink
-> main


=== main ===

Hi I am <color=\#F8FF30>Chuck Cluckers</color>, you have chosen my story! # speaker: Chuck Cluckers #portrait: Chuck_Neutral
This will be further dialogue for chuck. #speaker:Stellar Simon #portrait: Simon_Neutral
-> choice1

=== choice1 ===
How do you want the AI to react to you? #speaker:Chuck Cluckers #portrait: Chuck_Neutral
+ [Angry]
~ AI_Relation = 0
-> chosen ("Angry")
+ [Friendly]
~ AI_Relation = 10
-> chosen("Friendly")

=== chosen (relation) ===
~ Test_Value = relation
you chose <i>{Test_Value}!</i>
that is a brilliant choice. Well done.
{AI_Relation <5: -> angry | AI_Relation >5: -> friendly}


=== angry ===
~scene = "BridgeSetup"
~newScene = true
~newScene = false
I am the Ship AI. You are a threat and will be eliminated.
Goodbye.

-> END



=== friendly ===
~scene = "BridgeSetup"
~newScene = true
~newScene = false
I am the ship AI. Welcome aboard <color=\#F8FF30>Chuck Cluckers</color>

-> END