using FlexiTeams.DataClasses.Resource;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.UI.ResourceUI.ContentFiller
{
    internal class ProfessionalInfoFiller
    {
        public List<GameObject> TextObjects = new();

        private readonly GameObject _content;
        private readonly Resource _resource;
        private readonly UISettings _settings;

        public ProfessionalInfoFiller(GameObject content, Resource resource, UISettings settings) {

            _content = content;
            _resource = resource;
            _settings = settings;

            FillInData();
        }

        private void FillInData() {

            //required properties
            string professions = "- ";
            _resource.Professions.ForEach(s => { professions += s.ToString() + " / "; });
            professions = professions[..^3];

            string departments = "- ";
            _resource.Departments.ForEach(s => { departments += s.ToString() + " / "; });
            departments = departments[..^3];

            string workAgreement = "- ";

            string weeklyHours = "- "+ _settings.Language.ResourceUI.WeeklyHours + ": " + _resource.WeeklyHours.Hours + " " + _settings.Language.Hours;

            string yearlyTimeOf = "- " + _settings.Language.ResourceUI.YearlyTimeOf + ": " + _resource.YearlyTimeOf.Days + " " + _settings.Language.Days;

            string meansOfTransport = "- means of transport: ";
            _resource.MeansOfTransport.ForEach(s => { meansOfTransport += s.ToString() + " / "; });
            meansOfTransport = meansOfTransport[..^3];

            string commuteTime = "- " + _resource.CommuteTime.Minutes + " " + _settings.Language.Minutes;

            TextObj(professions);
            TextObj(departments);
            TextObj(weeklyHours);
            TextObj(yearlyTimeOf);
            TextObj(meansOfTransport);
            TextObj(commuteTime);

            //optional properties
            if (_resource.WorkExperience != null)
            {
                string workExperience =     "- " +
                                            _settings.Language.ResourceUI.WorkExperience + ": " +
                                            _resource.WorkExperience.Years + " " +
                                            _settings.Language.Years;
                TextObj(workExperience);
            }

            if (_resource.Overtime !=  null)
            {
                string overTime =   "- " + 
                                    _settings.Language.ResourceUI.Overtime + ": " +
                                    _resource.Overtime.Hours + " " +
                                    _settings.Language.Hours;
                TextObj(overTime);
            }

            if(_resource.YearlyEducation != null)
            {
                string yearlyEducation =    "- " + 
                                            _settings.Language.ResourceUI.YearlyEducation + ": " + 
                                            _resource.YearlyEducation.Days + " " + 
                                            _settings.Language.Days;
                TextObj(yearlyEducation);
            }

            if(_resource.Trainings != null)
            {
                string trainings = "- ";
                _resource.Trainings.ForEach(s =>
                {
                    trainings += s + " / ";
                });
                trainings = trainings[..^3];
                TextObj(trainings);
            }

            if (_resource.TrainingDuration != null)
            {
                string trainingDuration =   "- " +
                                            _settings.Language.ResourceUI.TrainingDuration + ": " + 
                                            _resource.TrainingDuration.Years + " " + 
                                            _settings.Language.Years;
                TextObj(trainingDuration);
            }

            if(_resource.Qualifications != null)
            {
                string qualifications = "- ";
                _resource.Qualifications.ForEach(s =>
                {
                    qualifications += s + " / ";
                });
                qualifications = qualifications[..^3];
                TextObj(qualifications);
            }

            if(_resource.AdditionalJobs != null)
            {
                string additionalJobs = "- ";
                _resource.AdditionalJobs.ForEach(s =>
                {
                    additionalJobs += s;
                    if(s.YearlyRequiredDays != null)
                    {
                        additionalJobs += ": " + s.YearlyRequiredDays + " " + _settings.Language.Days;
                    }
                    additionalJobs += " / ";
                });
                additionalJobs = additionalJobs[..^3];
                TextObj(additionalJobs);
            }

            if(_resource.Studies != null)
            {
                string studies = "- ";
                _resource.Studies.ForEach(s =>
                {
                    studies += s;
                    if(s.Location != null)
                    {
                        studies += ": " + s.Location; 
                    }
                    studies = " / ";
                });
                studies= studies[..^3];
                TextObj(studies);
            }

            if(_resource.ProfessionalInfos != null)
            {
                string professionalInfos = "- ";
                _resource.ProfessionalInfos.ForEach(s =>
                {
                    professionalInfos += s + " / ";
                });
                professionalInfos = professionalInfos[..^3];
                TextObj(professionalInfos);
            }
        }

        private void TextObj(string text)
        {
            var textObj = new GameObject("Text") { layer = 5 };

            //Transform
            var transform = textObj.AddComponent<RectTransform>();
            transform.SetParent(_content.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.anchoredPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.pivot = new Vector2(0, 1);

            //TMP Pro
            var tmp = transform.AddComponent<TextMeshProUGUI>();
            tmp.text = text;

            TextObjects.Add(textObj);
        }
    }
}
