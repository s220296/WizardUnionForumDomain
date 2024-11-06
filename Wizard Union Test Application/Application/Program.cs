// WHAT PURPOSE DOES THIS APPLICATION SERVE?
//
// The purpose of this application is to be a social media forum where Wizards can create
// profiles and join unions to discuss current conflicts in the universe,
// share knowledge, create and attend meet-ups, and send messages.
// Functionality --
//   -Union profiles can be created
//     -Unions contain members
//     -Union members can have roles
//     -Unions can have entry requirements
//     -Unions have names
//     Unions can host message boards internally or between unions
//     
//   -Wizard profiles can be created
//     -Wizards have names
//     -Wizards have ages
//     -Wizards have places of birth
//     -Wizards can register spells
//     -Wizards have elemental masteries
//     -Wizards can join unions
//     Wizards can message on message boards
//     Wizards can message other wizards
//
//   -Places can be registered on the forum
//     -Places have cycle rates per eon, like universal time zones
//     -Places can be a part of other places, e.g. planet in a galaxy
//
//   -Spells can be registered on the forum
//     -Spells have attributes, e.g. cast time, casting speeches, casting actions, casting costs, etc.
//     -Spells have a creator/origin
//     -Spells have elemental classings

#define STANDARD_TEST
#define ALL_TEST
#define MESSAGE_TEST

#undef ALL_TEST
#undef STANDARD_TEST

using WU_Test;

#if MESSAGE_TEST || ALL_TEST
Console.WriteLine("MESSAGE_TEST\n");
MessagingTest.Run(0);
#endif

#if STANDARD_TEST || ALL_TEST
Console.WriteLine("STANDARD_TEST\n");
TestingProgram.Run(0);
#endif