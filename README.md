# Strive.WatsonToneAnalyzer
This API uses to IBM Watson Tone Analyzer to detect emotions in a text and keeps record of Strive App user emotions in user Journal database. 
<br>
The API is hosted at:  https://striveapi.azurewebsites.net/index.html
<br>
### API Documentation
<pre>
  Method: GET
  <br>
  Query Parameters: 
    "userId": 0,
    "text": "The user emotion description"
  <br>
  Usage Example: 
    https://striveapi.azurewebsites.net/GetToneAnalysis?userId={userId}&emotionDescription={emotion description text}
    The text description and userId is expected as part of the request url from the front-end User Interface application.
    <br>
  API Responce:
            [
              {
                "userId": 0,
                "date": "2020-09-26T18:56:17.408Z",
                "text": "string",
                "prevailingEmotion": "string",
                "joy": 0,
                "anger": 0,
                "sadness": 0,
                "fear": 0,
                "others": 0
              }
            ]
</pre>

#### The API project will be extended to support voice to text support, user management, security as well as a functionality to pick and recommend specific exercise types for specific types of emotions. 
This will involve API integration with third party providers and consumption of more IBM Watson AI services. 

##
