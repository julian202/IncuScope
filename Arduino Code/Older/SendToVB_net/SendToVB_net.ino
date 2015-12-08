
int val = 0; 
int prevval =0;
int total =0;
int SerialValue = 0;
int lighton = 0;
int count =0;
void setup() { 
  pinMode(13, OUTPUT);  
  //pinMode(6, OUTPUT); //Initiates Motor Channel A pin
  pinMode(7, OUTPUT); //Initiates Brake Channel A pin
  pinMode(6, INPUT); //Initiates Brake Channel A pin
  Serial.begin(9600); 
  digitalWrite(7, HIGH);
  digitalWrite(6, LOW);
  Serial.println(0);
}

//Sends the Number 1234 Over the Serial Port Once Every Second
void loop() {
  digitalWrite(13, LOW); 
  val = digitalRead(6);
  if (val != prevval)
  {
    total= total+1;
    digitalWrite(13, HIGH);
  }
  prevval=val;
  
  count=count+1;
  if (count == 9){
    count=0;
   
  SerialValue = Serial.read();
    if(SerialValue == 4){
      Serial.println(total);
      digitalWrite(13, HIGH); 
      delay(30);
    }
  
  }
  
 
  delay(5);
}







  //Serial.println(5011341);
  //delay(50);
