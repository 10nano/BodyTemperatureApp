namespace BodyTemperatureApp.UnitTests
{
    public class Tests
    {
        [Test]
        public void WhenAddAnyPatientInMemoryBodyTemp_ThenCorrectMinValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Pamiêta");
            patientMemory.AddBodyTemp(36.4f);
            patientMemory.AddBodyTemp(37.7f);
            patientMemory.AddBodyTemp(38.5f);
            patientMemory.AddBodyTemp(37.3f);
            patientMemory.AddBodyTemp(35.9f);

            // Act
            var statistics = patientMemory.GetStatistics();

            // Assert
            Assert.That(statistics.Min, Is.EqualTo(35.9f));
        }

        [Test]
        public void WhenAddAnyPatientInMemoryBodyTemp_ThenCorrectMaxValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Pamiêta");
            patientMemory.AddBodyTemp(40.7f);
            patientMemory.AddBodyTemp(41.2f);
            patientMemory.AddBodyTemp(39.8f);
            patientMemory.AddBodyTemp(38f);
            patientMemory.AddBodyTemp(37.3f);

            // Act
            var statistics = patientMemory.GetStatistics();

            // Assert
            Assert.That(statistics.Max, Is.EqualTo(41.2f));
        }

        [Test]
        public void WhenAddAnyPatientInMemoryBodyTemp_ThenCorrectRisesValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Pamiêta");
            patientMemory.AddBodyTemp(35.9f);
            patientMemory.AddBodyTemp(36.4f);
            patientMemory.AddBodyTemp(37f);
            patientMemory.AddBodyTemp(38f);
            patientMemory.AddBodyTemp(39.3f);

            // Act
            var statistics = patientMemory.GetStatistics();

            // Assert
            Assert.That(statistics.Rises, Is.EqualTo(true));
            Assert.That(statistics.NotRises, Is.EqualTo(false));
        }

        [Test]
        public void WhenAddAnyPatientInMemoryBodyTemp_ThenCorrectNotRisesValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Pamiêta");
            patientMemory.AddBodyTemp(39.9f);
            patientMemory.AddBodyTemp(38.4f);
            patientMemory.AddBodyTemp(37f);
            patientMemory.AddBodyTemp(37f);
            patientMemory.AddBodyTemp(36.3f);

            // Act
            var statistics = patientMemory.GetStatistics();

            // Assert
            Assert.That(statistics.NotRises, Is.EqualTo(true));
            Assert.That(statistics.Rises, Is.EqualTo(false));
        }
        [Test]
        public void WhenAddAnyPatientInFileBodyTemp_ThenCorrectMinValue()
        {
            // Arrange
            var patientFile = new PatientInMemory("Ewa Plikowa");
            patientFile.AddBodyTemp(36.4f);
            patientFile.AddBodyTemp(37.7f);
            patientFile.AddBodyTemp(38.5f);
            patientFile.AddBodyTemp(37.3f);
            patientFile.AddBodyTemp(35.9f);

            // Act
            var statistics = patientFile.GetStatistics();

            // Assert
            Assert.That(statistics.Min, Is.EqualTo(35.9f));
        }

        [Test]
        public void WhenAddAnyPatientInFileBodyTemp_ThenCorrectMaxValue()
        {
            // Arrange
            var patientFile = new PatientInMemory("Ewa Plikowa");
            patientFile.AddBodyTemp(40.7f);
            patientFile.AddBodyTemp(41.2f);
            patientFile.AddBodyTemp(39.8f);
            patientFile.AddBodyTemp(38f);
            patientFile.AddBodyTemp(37.3f);

            // Act
            var statistics = patientFile.GetStatistics();

            // Assert
            Assert.That(statistics.Max, Is.EqualTo(41.2f));
        }

        [Test]
        public void WhenAddAnyPatientInFileBodyTemp_ThenCorrectRisesValue()
        {
            // Arrange
            var patientFile = new PatientInMemory("Ewa Plikowa");
            patientFile.AddBodyTemp(35.9f);
            patientFile.AddBodyTemp(36.4f);
            patientFile.AddBodyTemp(37f);
            patientFile.AddBodyTemp(38f);
            patientFile.AddBodyTemp(39.3f);

            // Act
            var statistics = patientFile.GetStatistics();

            // Assert
            Assert.That(statistics.Rises, Is.EqualTo(true));
            Assert.That(statistics.NotRises, Is.EqualTo(false));
        }

        [Test]
        public void WhenAddAnyPatientInFileBodyTemp_ThenCorrectNotRisesValue()
        {
            // Arrange
            var patientFile = new PatientInMemory("Ewa Plikowa");
            patientFile.AddBodyTemp(39.9f);
            patientFile.AddBodyTemp(38.4f);
            patientFile.AddBodyTemp(37f);
            patientFile.AddBodyTemp(37f);
            patientFile.AddBodyTemp(36.3f);

            // Act
            var statistics = patientFile.GetStatistics();

            // Assert
            Assert.That(statistics.NotRises, Is.EqualTo(true));
            Assert.That(statistics.Rises, Is.EqualTo(false));
        }
    }
}