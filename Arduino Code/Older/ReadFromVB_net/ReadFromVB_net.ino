int SerialValue = 0;

void setup(){
  
  pinMode(12, OUTPUT);
  pinMode(7, OUTPUT);
  Serial.begin(9600);
  
}

void loop(){
  SerialValue = Serial.read();
  if(SerialValue == 12){
    digitalWrite(12, HIGH);
  }
   if(SerialValue == 7){
    digitalWrite(7, HIGH);
    
    
  }
  if(SerialValue == 0){
    digitalWrite(12, LOW);
    digitalWrite(7, LOW);
    
  }

}
