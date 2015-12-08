
int val = 0; 
int prevval =0;
long total =3;
int SerialValue = 0;
int lighton = 0;
int count =0;

void setup(){
  
 //Setup Channel A
  pinMode(12, OUTPUT); //Initiates Motor Channel A pin
  pinMode(9, OUTPUT); //Initiates Brake Channel A pin
  pinMode(7, OUTPUT); //Initiates Brake Channel A pin
  pinMode(6, INPUT); //Initiates Brake Channel A pin
  digitalWrite(7, HIGH);
  digitalWrite(6, LOW);
  Serial.begin(1000000);
  
}

void loop(){
  val = digitalRead(6);
  
  SerialValue = Serial.read();
  /*
  if(SerialValue > 3){
    digitalWrite(12, HIGH); //Establishes forward direction of Channel A
    digitalWrite(9, LOW);   //Disengage the Brake for Channel A
    analogWrite(3, 155);   //Spins the motor on Channel A at full speed /with 9V power supply pwm 60 is around 2V, and pwm 255 is around 6.6V.
    }
  */
  
  if (SerialValue>total){   
    Serial.print("*morethantotal");
    digitalWrite(12, HIGH); //Establishes forward direction of Channel A
    digitalWrite(9, LOW);   //Disengage the Brake for Channel A
    analogWrite(3, 155);   //Spins the motor on Channel A 
    if (val != prevval){
      total= total+1;
    }
  }
  else{
     digitalWrite(9, HIGH); //Eengage the Brake for Channel A
  }
  if (SerialValue==0){  
    digitalWrite(9, HIGH); //Eengage the Brake for Channel A
  }
  
  
    
 prevval=val;
 Serial.print("*");
 delay(250);

}


 //digitalWrite(13, HIGH);




