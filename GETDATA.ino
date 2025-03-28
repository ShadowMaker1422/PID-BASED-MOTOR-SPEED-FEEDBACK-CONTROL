#include<string.h>
#include<stdio.h>

void setup() {
  pinMode(13, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  String data ;
  while (Serial.available() > 0) {
    char c = Serial.read();
    data += c;
    delay(5);
  }
  data.trim();
  
  // Sử dụng mảng động để lưu trữ các token
  const int maxTokens = 10; // Số lượng token tối đa
  char* tokens[maxTokens];
  int numTokens = 0;

  
  char* dataPtr = const_cast<char*>(data.c_str()); // Chuyển đổi kiểu dữ liệu
  char* token = strtok(dataPtr, " ");
  while (token != NULL && numTokens < maxTokens) {
    tokens[numTokens] = token;
    numTokens++;
    token = strtok(NULL, " ");
    }

/*char* token = strtok(data.c_str(), " ");
  while (token != NULL && numTokens < maxTokens) {
    tokens[numTokens] = token;
    numTokens++;
    token = strtok(NULL, " ");
  }
*/   
//Serial.println(atof(tokens[0]));

  // Kiểm tra và thực hiện các hành động tương ứng
  if (data == "ON") {
    digitalWrite(13, HIGH);
  }
  if (data == "OFF") {
    digitalWrite(13, LOW);
  }
 /* if (numTokens >= 2 && strcmp(tokens[0], "1") == 0) {
    digitalWrite(13, HIGH);
  }
  if (numTokens >= 2 && strcmp(tokens[1], "1") == 0) {
    digitalWrite(13, LOW);
  }
 if (numTokens >= 2 && strcmp(tokens[2], "1") == 0) {
    digitalWrite(13, HIGH);
  }
  if (numTokens >= 2 && strcmp(tokens[3], "1") == 0) {
    digitalWrite(13, LOW);
  }*/
  //CONVERT STRING INTO DOUBLE BY USING ATOF()
  /*if(atof(tokens[0])==1){
    digitalWrite(13,HIGH);
  delay(1000);
  digitalWrite(13,LOW);
  delay(1000);
  digitalWrite(13,HIGH);
  }*/
  for (int i = 0; i < 5; i++) {
    Serial.println(atof(tokens[0]));
    delay(1000);
   
  }
 
  
}
