String incomingString;
char incomingSerialValue;
char firstChar;
char secondChar;
String incomingStringStepper;
long incomingLongStepper;

void setup(){ 
  //pinMode(13, OUTPUT);
  //Stepper1:
  pinMode(4, OUTPUT);
  digitalWrite(4, HIGH); //DIRECTION
  pinMode(5, OUTPUT);
  digitalWrite(5, HIGH); //STEP
  pinMode(2, OUTPUT);
  digitalWrite(2, HIGH); //ENABLE PIN (HIGH means DISABLED!)
 
  
  //Stepper2: can't use because pin 6 is used by darkfield light.
  /*pinMode(6, OUTPUT);
  digitalWrite(6, HIGH); //STEP*/
  pinMode(7, OUTPUT);
  digitalWrite(7, HIGH); //DIRECTION
  pinMode(A1, OUTPUT);
  digitalWrite(A1, HIGH); //ENABLE PIN (HIGH means DISABLED!)
  
  //Lights low power LED:
  pinMode(A4, OUTPUT);
  pinMode(A3, OUTPUT);  
  
  //Lights high power LED:
  pinMode(3, OUTPUT); //fl cam1
  pinMode(6, OUTPUT); //df
  pinMode(9, OUTPUT); //fl cam2
  
  

  Serial.begin(115200);  
}
void loop(){
  
   // send data only when you receive data:
  if (Serial.available() > 0) {
    
    incomingString="";  //important.
    while(Serial.available()) {
      incomingSerialValue = Serial.read();
      incomingString.concat(incomingSerialValue);
      delay (10);
    } 
    firstChar=incomingString.charAt(0);
    secondChar=incomingString.charAt(1);
 
      
    //LIGHTS
    if (firstChar == 'l'){  //this is L not 1. //light.
      if (secondChar == 'r'){
         digitalWrite(A3, HIGH);
         //digitalWrite(13, HIGH);
      }
      else {
         digitalWrite(A4, HIGH);
         
      }
      //Serial.println("light on");
      firstChar = '0';
    }
    
    if (firstChar == '1'){  //this is 1 not L. //light on only for fluorescence in leedseeduino shield
      digitalWrite(3, HIGH);
    }
    if (firstChar == '2'){  //light on only for fluorescence in leedseeduino shield 
      digitalWrite(6, HIGH);
    }
    if (firstChar == '3'){  //light on only for fluorescence in leedseeduino shield 
      digitalWrite(9, HIGH);
    }
    
    
    
    if (firstChar == 'f'){  //light.
      //digitalWrite(13, LOW);
      digitalWrite(A3, LOW);
      digitalWrite(A4, LOW);
      //digitalWrite(A1, HIGH); //only for fluorescence in A4988.
      digitalWrite(3, LOW);
      digitalWrite(9, LOW);
      digitalWrite(6, LOW);
      
      //Serial.println("light off");
      firstChar = '0';
    }
    
    
    
    if (firstChar == 'e'){  //stepper.
      digitalWrite(2, LOW); 
      Serial.println("stepper enabled");
      firstChar = '0';
    }
    if (firstChar == 'd'){  //stepper.
      digitalWrite(2, HIGH); 
      Serial.println("stepper disabled");
      firstChar = '0';
    }
    
    if (firstChar == 'm'){  // move stepper.
      if (secondChar == 'f'){  //forward direction
       digitalWrite(4, HIGH);
      }
      else { //backward direction
        digitalWrite(4, LOW);
      }
      

      incomingStringStepper=incomingString.substring(2);
      incomingLongStepper=incomingStringStepper.toInt();
      Serial.println("moving stepper");
      while (incomingLongStepper>0) {
        //Serial.println(incomingLongStepper);
        digitalWrite(5, HIGH);  //use A5 instead of 5 for previous non-dual setup
        delay(2);
        digitalWrite(5, LOW); //use A5 instead of 5 for previous non-dual setup
        delay(2);
        incomingLongStepper=incomingLongStepper-1;
      }
      Serial.println("Stopped");
      firstChar = '0';
    }
    
    
  }
  
}
