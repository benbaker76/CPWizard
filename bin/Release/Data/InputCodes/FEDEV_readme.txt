FEDEV_descriptions_to_controls.txt
This is a | delimited file with 2 fields.
Th first field is the control description.
The second field is the control constant that control uses.
If a control description has more than one constant there will
be more than one line.  It's modeled after a database table.
Example, an 8-way optical joystick has both dial and joy8way.  
You will see two entries for 8-way optical joystick simular to this:
8-way Rotory Joystick(Optical)|joy8way
8-way Rotory Joystick(Optical)|dial


FEDEV_controls_to_labels.txt
This | delimited file has 4 fields.
The first field is control constants, these will match the control 
constants in the second field of FEDEV_descriptions_to_controls.txt
The second field is the mame label (as seen in std.ini for the ctrlr files)
but without the P#_ prefix where # is player number.
The third field is a generic label.  This is used in the wizards to fill 
out textboxes with default.  You probably won't have a need for this.
** Depricated **
The forth field is a replace text field.  Right now only stickaxis (which
only Throttle uses).  The format is Replace_Text-option1:option2
Replace-Text is the text in the label to replace.  The options are what it
could be replaced with.
Note stickaxis looks like:
stickaxis|AD_STICK_Axis|Up|Axis-X:Y:Z
AD_STICK_Axis is the label.  Axis is the replace text.  X, Y, and Z are
the options.  In the wizard if a user adds Throttle (which uses stickaxis)
The replace text created a drop down box with the options.  Then Axis
is replaced with that option.  So, like in aburner the throttle is on the
Z-axis.  The label in the controls INI or XML file will be P1_AD_STICK_Z.




INI Format
P#Controls format:
Each control is seperated by a |
Each control has three sections (1st two required)
The sections are seperated by +
In each section can be a list seperated by &
The first section is the control description
The second section is the control constants for the description which are found in FEDEV_descriptions_to_controls.txt
The third section is buttons associated with the control.
So, if a game uses an 8-way Triggerstick with 2 buttons and an optical rotory joystick would look like this
P1Controls=8-way Joystick+joy8way+P1_BUTTON1&P1_BUTTON2|8-way Rotory Joystick (Optical)+joy8way&dial
Note how a 3rd section does not have to be there if the control doesn't have buttons on it.

examples:

[playch10]
gamename=PlayChoice-10
numPlayers=2
alternating=0
mirrored=0
tilt=0
cocktail=0
usesService=1
miscDetails=The playchoice 10 system is a pay-per-play system based on the original nes console and plays the same titles.  Instead of purchasing lives, you purchase time on the machine.  The machine can hold 10 games at once and you switch using the game menu on the secondary monitor.   
P1NumButtons=2
P1Controls=8-way Joystick+joy8way|Lightgun+lightgun
P1_BUTTON2=B
P1_BUTTON1=A
P1_JOYSTICK_RIGHT=Right
P1_JOYSTICK_LEFT=Left
P1_JOYSTICK_DOWN=Down
P1_JOYSTICK_UP=Up
P1_LIGHTGUN_X_EXT=Right
P1_LIGHTGUN_X=Left
P1_LIGHTGUN_Y=Up
P1_LIGHTGUN_Y_EXT=Down
P2NumButtons=2
P2Controls=8-way Joystick+joy8way
P2_BUTTON1=A
P2_BUTTON2=B
P1_JOYSTICK_UP=Up
P1_JOYSTICK_DOWN=Down
P1_JOYSTICK_LEFT=Left
P1_JOYSTICK_RIGHT=Right
SERVICE1=Service Coin
SERVICE2=Channel Select
SERVICE3=Enter
SERVICE4=Reset

[aquajack]
gamename=Aqua Jack (World)
numPlayers=1
alternating=0
mirrored=0
tilt=1
cocktail=0
usesService=0
miscDetails=In mame a dial is also emulated.  This dial has no apparent function and isn't used in the game at all.  It could be reminants of a hack or something.
P1NumButtons=4
P1Controls=Pedal (Microswitch)+button+P1_BUTTON4|8-way Triggerstick+joy8way+P1_BUTTON3&P1_BUTTON2&P1_BUTTON1
P1_BUTTON3=Vulcan
P1_BUTTON2=Jump
P1_BUTTON1=Machine Gun
P1_BUTTON4=Thrust
P1_JOYSTICK_RIGHT=Right
P1_JOYSTICK_LEFT=Left
P1_JOYSTICK_DOWN=Down
P1_JOYSTICK_UP=Up




XML Format

DTD is listed below.  There are some important things to point out.
In the DTD you see that both game and player elements can have labels.
In the game element the only labels that should be listed is if the game
used service buttons for menu navigation (I.E. PlayChoice 10).
Most of it should be straight forward, however I will clarify the game 
elements attributes:

<!ATTLIST game romname CDATA #IMPLIED>
romname is the romname :)

<!ATTLIST game gamename CDATA #IMPLIED>
Game name is the name of the game as MAME has it listed.

<!ATTLIST game numPlayers CDATA #IMPLIED>
numPlayers is the number of player the game has.
 
<!ATTLIST game mirrored CDATA #IMPLIED>
mirrored is if all players use the ame labels as player 1.

<!ATTLIST game alternating CDATA #IMPLIED>
alternating is if the game used alternating players.  mirrored is implied.
In cocktail games if the cocktail dipswitch is set it will use player 2 
controls, either way since mirrored is implied you will be using player 1
labels anyway.

<!ATTLIST game usesService CDATA #IMPLIED>
usesService is if the game uses service buttons for menu navigation.  
Most multigame systems will use this like PlayChoice 10.  
The buttons are SERVICE1-SERVICE4.

<!ATTLIST game tilt CDATA #IMPLIED>
tilt is if the game uses tilt.  Some people do implement tilt on his/her
cabinet.  In the ctrlr files TILT is the label used.

<!ATTLIST game cocktail CDATA #IMPLIED>
cocktail is if the game had a cocktail dipswitch.  Useful with cocktail FEs and determinining how to diplay control information for these games.


XML DTD:
    <!ELEMENT dat (meta,game*)>
    <!ELEMENT meta (description,version,time,generatedBy)>
    <!ELEMENT description EMPTY>
        <!ATTLIST description name CDATA #IMPLIED>
    <!ELEMENT version EMPTY>
        <!ATTLIST version name CDATA #IMPLIED>
    <!ELEMENT time EMPTY>
        <!ATTLIST time name CDATA #IMPLIED>
    <!ELEMENT generatedBy EMPTY>
        <!ATTLIST generatedBy name CDATA #IMPLIED>
    <!ELEMENT game (miscDetails,label*,player+)>
        <!ATTLIST game romname CDATA #IMPLIED>
        <!ATTLIST game gamename CDATA #IMPLIED>
        <!ATTLIST game numPlayers CDATA #IMPLIED>
        <!ATTLIST game alternating CDATA #IMPLIED>
        <!ATTLIST game mirrored CDATA #IMPLIED>
        <!ATTLIST game usesService CDATA #IMPLIED>
        <!ATTLIST game tilt CDATA #IMPLIED>
        <!ATTLIST game cocktail CDATA #IMPLIED>
    <!ELEMENT miscDetails (#PCDATA)>
    <!ELEMENT label EMPTY>
        <!ATTLIST label name CDATA #IMPLIED>
        <!ATTLIST label value CDATA #IMPLIED>
    <!ELEMENT player (controls*,labels*)>
        <!ATTLIST player number CDATA #IMPLIED>
        <!ATTLIST player numButtons CDATA #IMPLIED>
    <!ELEMENT controls (control*)>
    <!ELEMENT labels (label*)>
    <!ELEMENT control  (constant*,button*)>
        <!ATTLIST control name CDATA #IMPLIED>
    <!ELEMENT constant EMPTY>
        <!ATTLIST constant name CDATA #IMPLIED>
    <!ELEMENT button EMPTY>
        <!ATTLIST button name CDATA #IMPLIED>

Some XML node examples:

<dat>
    <game romname="aquajack" gamename="Aqua Jack (World)" numPlayers="1" alternating="0" mirrored="0" usesService="0" tilt="1" cocktail="0">
        <miscDetails>
In mame a dial is also emulated.  This dial has no apparent function and isn't used in the game at all.  It could be reminants of a hack or something.
        </miscDetails>
        <player number="1" numButtons="4">
            <controls>
                <control name="Pedal (Microswitch)">
                    <constant name="button"/>
                    <button name="P1_BUTTON4"/>
                </control>
                <control name="8-way Triggerstick">
                    <constant name="joy8way"/>
                    <button name="P1_BUTTON3"/>
                    <button name="P1_BUTTON2"/>
                    <button name="P1_BUTTON1"/>
                </control>
            </controls>
            <labels>
                <label name="P1_BUTTON3" value="Vulcan"/>
                <label name="P1_BUTTON2" value="Jump"/>
                <label name="P1_BUTTON1" value="Machine Gun"/>
                <label name="P1_BUTTON4" value="Thrust"/>
                <label name="P1_JOYSTICK_RIGHT" value="Right"/>
                <label name="P1_JOYSTICK_LEFT" value="Left"/>
                <label name="P1_JOYSTICK_DOWN" value="Down"/>
                <label name="P1_JOYSTICK_UP" value="Up"/>
            </labels>
        </player>
    </game>
</dat>
