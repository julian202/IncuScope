
int val = 0; 
int prevval =0;
int total =0;
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
  Serial.begin(9600);
  
}

void loop(){
  
  val = digitalRead(6);
  if (val != prevval){
    total= total+1;
    digitalWrite(13, HIGH);
  }
  prevval=val;
  
  count=count+1;
  
     
  SerialValue = Serial.read();
  if(SerialValue == 1){
    digitalWrite(12, HIGH); //Establishes forward direction of Channel A
    digitalWrite(9, LOW);   //Disengage the Brake for Channel A
    analogWrite(3, 155);   //Spins the motor on Channel A at full speed /with 9V power supply pwm 60 is around 2V, and pwm 255 is around 6.6V.
    }
   if(SerialValue == 2){
    digitalWrite(12, LOW); //Establishes backward direction of Channel A
    digitalWrite(9, LOW);   //Disengage the Brake for Channel A
    analogWrite(3, 155);   //Spins the motor on Channel A at full speed /with 9V power supply pwm 60 is around 2V, and pwm 255 is around 6.6V.
  }
  if(SerialValue == 3){
    digitalWrite(9, HIGH); //Eengage the Brake for Channel A
  }
  if(SerialValue == 4){
   // if (count == 9){
     //count=0;
     
      Serial.println(total);
      //digitalWrite(13, HIGH); 
      //delay(30);
    //}
  }
   //Serial.println(total);
   //digitalWrite(13, HIGH); 
   //delay(30);
  //delay(10);

}
