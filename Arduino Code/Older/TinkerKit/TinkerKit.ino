
// include the TinkerKit library
//#include <TinskerKit.h>
//#include <TinkerKit.h>
// creating the object 'led' that belongs to the 'TKLed' class 

//TKLed led(O5);  //O4 (Output4)of the tinkershield is pin 5 of arduino motor shield

// you can do #define O4 5
int SerialValue = 0;
void setup() {
//nothing here
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
 
  Serial.begin(9600);
}
 
void loop() 
{
  digitalWrite(5, LOW); 
  //led.off();  
  SerialValue = Serial.read();
  if(SerialValue == 12){
    digitalWrite(5, HIGH);
  }
  if(SerialValue == 7){
   digitalWrite(6, HIGH);    
  }
  if(SerialValue == 0){
   digitalWrite(5, LOW);  
   digitalWrite(6, LOW);    
  }
  
  //led.brightness(1023);
  //led.on();       // set the LED on
//  delay(1000);    // wait for a second
//  led.off();      // set the LED off
//  delay(500);    // wait for a second
//  led.brightness(0);
//   led.on();       // set the LED on
//  delay(500);    // wait for a second
//  led.off();      // set the LED off
//  delay(500);
}
