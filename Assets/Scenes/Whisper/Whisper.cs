using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OpenAI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Whisper
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;
        
        private readonly string fileName = "output.wav";
        private readonly int duration = 5;
        public Chat chat1;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();
        private List<ChatMessage> messages = new List<ChatMessage>();
        
        
        private void Start()
        {
            StartCoroutine(RequestMicrophoneAccess());
            #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
            #else
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
            #endif
        }

        IEnumerator RequestMicrophoneAccess()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);

            if (Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                Debug.Log("Microphone access granted");
                // Continue with your application logic here
            }
            else
            {
                Debug.Log("Microphone access denied");
                // Handle denial of microphone access
            }
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }
        
        private void StartRecording()
        {
            string[] microphones = Microphone.devices;

            if (Microphone.devices.Length == 0)
            {
                message.text = "Error: No microphone devices available.";
                return;
            }

            isRecording = true;
            recordButton.enabled = false;

            int index = PlayerPrefs.GetInt("user-mic-device-index");
            
            #if !UNITY_WEBGL
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
            #endif
        }

        private async void EndRecording()
        {
            message.text = "Transcripting...";
            
            #if !UNITY_WEBGL
            Microphone.End(null);
            #endif
            
            if (clip == null)
    {
        message.text = "How to differentiate open wound and close wound?";
        chat1.reply();
        recordButton.enabled = true;
        return;
    }

            byte[] data = SaveWav.Save(fileName, clip);

            if (data == null || data.Length == 0)
            {
                message.text = "Error: Audio data is empty.";
                recordButton.enabled = true;
                return;
            }

            var req = new CreateAudioTranslationRequest
            {
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                // File = Application.persistentDataPath + "/" + fileName,
                Model = "whisper-1",
                
            };
            var res = await openai.CreateAudioTranslation(req);

            progressBar.fillAmount = 0;
            string transcribedText = res.Text;
            message.text = transcribedText;
            // Send the transcribed text to the chatbot
            await GetChatbotResponse(transcribedText);
            message.text = res.Text;
            recordButton.enabled = true;
        }

        private async System.Threading.Tasks.Task GetChatbotResponse(string userInput)
        {
            message.text = "Chatbot is thinking...";
            ChatMessage newMessage = new ChatMessage();
            newMessage.Content = userInput;
            newMessage.Role = "user";

            messages.Add(newMessage);

            CreateChatCompletionRequest request = new CreateChatCompletionRequest();
            request.Messages = messages;
            request.Model = "gpt-3.5-turbo";

            var response = await openai.CreateChatCompletion(request);

            if (response.Choices != null && response.Choices.Count > 0)
            {
                var chatResponse = response.Choices[0].Message;
                messages.Add(chatResponse);
                
                Debug.Log(chatResponse.Content);
            }
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;
                progressBar.fillAmount = time / duration;
                
                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}

public static class SaveWav
{
    public static byte[] Save(string filename, AudioClip clip)
    {
        using (var fileStream = CreateEmpty(filename))
        {
            ConvertAndWrite(fileStream, clip);
            WriteHeader(fileStream, clip);
            return File.ReadAllBytes(filename);
        }
    }

    private static FileStream CreateEmpty(string filename)
    {
        var fileStream = new FileStream(filename, FileMode.Create);
        for (int i = 0; i < 44; i++) // Space for the WAV header
        {
            fileStream.WriteByte(0);
        }
        return fileStream;
    }

    private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);
        Int16[] intData = new Int16[samples.Length];
        Byte[] bytesData = new Byte[samples.Length * 2];

        int rescaleFactor = 32767; // To convert float to Int16

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }
        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    private static void WriteHeader(FileStream fileStream, AudioClip clip)
    {
        fileStream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(one);
        fileStream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(clip.frequency);
        fileStream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(clip.frequency * 2);
        fileStream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = (ushort)(16);
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(clip.samples * 2);
        fileStream.Write(subChunk2, 0, 4);
    }
}
