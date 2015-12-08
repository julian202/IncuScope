
char incomingSerialValue;
String incomingString;
String incomingStringX;
String incomingStringY;
String incomingStringStepper;
long incomingLongX;
long incomingLongY;
long incomingLongStepper;
long totalCountX=0;
long totalCountY=0;
int encoderValueX=0;
int encoderValueY=0;
int prevEncoderValueX=0;
int prevEncoderValueY=0;
boolean goingForwardX;
boolean goingForwardY;
char firstChar;
char secondChar;
boolean finishedMovingX=false;
boolean finishedMovingY=false;
boolean doneCount=false;
long finishCount=0;
//long TESTCOUNT=0;
//long TESTCOUNT2=0;
void setup(){ 
  //STEPPER
  pinMode(2, OUTPUT);
  pinMode(A5, OUTPUT);   
  pinMode(A4, OUTPUT);
  pinMode(A3, OUTPUT);  
  pinMode(4, OUTPUT);
  pinMode(10, OUTPUT); 
  digitalWrite(A5, HIGH); //STEP
  digitalWrite(2, HIGH); //ENABLE
  digitalWrite(4, HIGH); //DIRECTION
  digitalWrite(10, HIGH); //MICROSTEPPING
  //
  pinMode(11, OUTPUT);
  pinMode(3, OUTPUT);   
  pinMode(12, OUTPUT);
  pinMode(9, OUTPUT); 
  pinMode(13, OUTPUT);
  pinMode(8, OUTPUT);
  pinMode(7, INPUT); 
  pinMode(6, INPUT); 
  //digitalWrite(7, LOW);
  //digitalWrite(6, LOW);
  pinMode(5, OUTPUT); //Light on pin.
  //digitalWrite(5, HIGH); 
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
    //incomingString=incomingString.substring(0,3);
    //incomingString=incomingString.substring(1); 
    //Serial.println(incomingString);
    
    if (firstChar == 'x'){
      incomingStringX=incomingString.substring(1,incomingString.indexOf('y')); //1 is actually the second one.
      incomingStringY=incomingString.substring(incomingString.indexOf('y')+1); //is from 'y' to end.
      incomingLongX=incomingStringX.toInt();
      incomingLongY=incomingStringY.toInt();
      doneCount=false;
      Serial.print("Moving to X,Y: ");
      Serial.print(incomingLongX);
      Serial.print(","); 
      Serial.println(incomingLongY);
      
      if (incomingLongX > totalCountX){    
        digitalWrite(12, HIGH); //Establishes forward direction of Channel A
        goingForwardX=true;
      }    
      else{
        digitalWrite(12, LOW); //Establishes backward direction of Channel A
        goingForwardX=false;    
      }
      digitalWrite(9, LOW);   //Disengage the Brake for Channel A
      analogWrite(3, 255);   //Spins the motor on Channel A  
          
      if (incomingLongY > totalCountY){    
        digitalWrite(13, LOW); //Establishes forward direction of Channel B
        goingForwardY=true;
      }    
      else{
        digitalWrite(13, HIGH); //Establishes backward direction of Channel B
        goingForwardY=false;    
      }
      digitalWrite(8, LOW);   //Disengage the Brake for Channel B
      analogWrite(11, 255);   //Spins the motor on Channel B   
        
    }
    
    if (firstChar == 'c'){  //get current position.
        Serial.print("X ");
        Serial.print(totalCountX);
        Serial.print(" Y ");
        Serial.println(totalCountY);
        firstChar = '0';
      }

    if (firstChar == 'z'){  //zero.
      totalCountX=0;
      totalCountY=0;
      Serial.print("X ");
      Serial.print(totalCountX);
      Serial.print(" Y ");
      Serial.println(totalCountY);
      firstChar = '0';
    }
    
    //LIGHTS
    if (firstChar == 'l'){  //light.
      if (secondChar == 'r'){
         digitalWrite(A3, HIGH);
      }
      else {
         digitalWrite(A4, HIGH);
      }
      Serial.println("light on");
      firstChar = '0';
    }
    if (firstChar == 'f'){  //light.
      digitalWrite(A3, LOW);
      digitalWrite(A4, LOW); 
      Serial.println("light off");
      firstChar = '0';
    }
    if (firstChar == 'e'){  //stepper.
      digitalWrite(2, LOW); 
      Serial.println("stepper enabled");
      firstChar = '0';
    }
    if (firstChar == 'd'){  //stepper.
      digitalWrite(2, HIGH); 
      Serial.println("stepper diabled");
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
        digitalWrite(A5, HIGH);
        delay(2);
        digitalWrite(A5, LOW);
        delay(2);
        incomingLongStepper=incomingLongStepper-1;
      }
      
      firstChar = '0';
    }
    
    
  }
  
  
  if (totalCountX == incomingLongX){
    digitalWrite(9, HIGH); //Engage the Brake for Channel A  
    finishedMovingX=true;  
  }
  encoderValueX = digitalRead(7); 
  if (encoderValueX != prevEncoderValueX){
    if (goingForwardX==true){    
      totalCountX= totalCountX+1;
    }
    else{
      totalCountX= totalCountX-1;
    }  
     prevEncoderValueX=encoderValueX;
  }    
  if (totalCountY == incomingLongY){
    digitalWrite(8, HIGH); //Engage the Brake for Channel B  
    finishedMovingY=true; 
  }     
  encoderValueY = digitalRead(6);
   //TESTCOUNT2=TESTCOUNT2+1;
  if (encoderValueY != prevEncoderValueY){
    if (goingForwardY==true){     
      totalCountY= totalCountY+1;
    }
    else{
      totalCountY= totalCountY-1;
    }  
     prevEncoderValueY=encoderValueY;
  }
  
  if  ((finishedMovingX==true) && (finishedMovingY==true)){
    finishCount=finishCount+1;
    if (finishCount==50000){
      //Serial.print(TESTCOUNT2);
      Serial.print("Stopped at ");
      Serial.print(totalCountX);
      Serial.print(",");
      Serial.println(totalCountY);          
      finishedMovingX=false;
      finishedMovingY=false;
      finishCount=0; 
      //TESTCOUNT2=0;
      firstChar = '0';  
      doneCount=true;
    }
  }
    
  
  
  /*
  TESTCOUNT=TESTCOUNT+1;
  if (TESTCOUNT==5){
    Serial.println("test START");
  }
  if (TESTCOUNT==1000000){
    Serial.println("test DONE");
    delay(2000);
    TESTCOUNT=0;
  }
  */
  
}//end void loop()


 //digitalWrite(13, HIGH);

 // if(SerialValue == 4){

//}
 // delay(300);
 // digitalWrite(9, HIGH); //Engage the Brake for Channel A
  

