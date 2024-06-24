using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechieBuddy.Wpf.Commands;
using TechieBuddy.Core.Services;
using PuppeteerSharp;
using System.IO;
using System.Reflection;

namespace TechieBuddy.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ILogger<MainWindowViewModel> _logger;
        private readonly ITechieBuddyAIService _techieBuddyAIService;
        private string _pageTemplate = string.Empty;

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger, ITechieBuddyAIService techieBuddyAIService)
        {
            _logger = logger;
            AskCommand = new RelayCommand(async param => await AskAsync(param));
            _techieBuddyAIService = techieBuddyAIService;

            LoadHtmlTemplate();
        }


        private void LoadHtmlTemplate()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ViewModels", "Template.html");

            if (File.Exists(filePath))
            {
                _pageTemplate = File.ReadAllText(filePath);
            }
            else
            {
                _logger.LogError("Template.html file not found at path: {FilePath}", filePath);
            }
        }

        private string _software;
        public string Software
        {
            get => _software;
            set
            {
                _software = value;
                OnPropertyChanged();
            }
        }

        private string _question;
        public string Question
        {
            get => _question;
            set
            {
                _question = value;
                OnPropertyChanged();
            }
        }

        private string _response;
        public string Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand AskCommand { get; }

        private async Task AskAsync(object parameter)
        {
            try
            {
                IsLoading = true;
                _logger.LogInformation("Ask command executed with Software: {Software}, Question: {Question}", Software, Question);

                var response = await _techieBuddyAIService.AnswerQuestionAsync(Software, Question);

                Response = _pageTemplate.Replace("__placeholder__", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing Ask command");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
