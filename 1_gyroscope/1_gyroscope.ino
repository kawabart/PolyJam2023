#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>
#define Button1 5
#define Button2 4

Adafruit_MPU6050 mpu;

void setup(void) {
  pinMode(Button1, INPUT_PULLUP);
  pinMode(Button2, INPUT_PULLUP);
  
  Serial.begin(115200);

  // Try to initialize!
  if (!mpu.begin()) {
    Serial.println("Failed to find MPU6050 chip");
    while (1) {
      delay(10);
    }
  }
  Serial.println("MPU6050 Found!");

  // set accelerometer range to +-8G
  mpu.setAccelerometerRange(MPU6050_RANGE_8_G);

  // set gyro range to +- 500 deg/s
  mpu.setGyroRange(MPU6050_RANGE_500_DEG);

  // set filter bandwidth to 21 Hz
  mpu.setFilterBandwidth(MPU6050_BAND_21_HZ);

//  delay(100);
}

void loop() {
  
// Serial.write("ABCD")
  /* Get new sensor events with the readings */
  sensors_event_t a, g, temp;
  mpu.getEvent(&a, &g, &temp);

  /* Print out the values */
//  Serial.print("Acceleration X: ");
//  Serial.print(a.acceleration.x);
//  Serial.print(", Y: ");
//  Serial.print(a.acceleration.y);
//  Serial.print(", Z: ");
//  Serial.print(a.acceleration.z);
//  Serial.println(" m/s^2");
//
//  Serial.print("Rotation X: ");
//  Serial.print(g.gyro.x);
//  Serial.print(", Y: ");
//  Serial.print(g.gyro.y);
//  Serial.print(", Z: ");
//  Serial.print(g.gyro.z);
//  Serial.println(" rad/s");
//
//  Serial.print("Temperature: ");
//  Serial.print(temp.temperature);
//  Serial.println(" degC");

  if(digitalRead(Button1) == LOW)
  {
    Serial.print(1);
    Serial.print(";");
  }
  else
  {
    Serial.print(0);
    Serial.print(";");
  }
  if(digitalRead(Button2) == LOW)
  {
    Serial.print(1);
    Serial.print(";");
  }
  else
  {
    Serial.print(0);
    Serial.print(";");
  }
  Serial.print(g.gyro.x);
  Serial.print(";");
  Serial.print(g.gyro.y);
  Serial.print(";");
  Serial.print(g.gyro.z);
  
  Serial.println("");
  delay(50);
}
