namespace BodyTemperatureApp.UnitTests
{
    public class Tests
    {
        [Test]
        public void WhenAddAnyPatientInMemBodyTemp_ThenCorrectMinValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Testowy");
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
        public void WhenAddAnyPatientInMemBodyTemp_ThenCorrectMaxValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Testowy");
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
        public void WhenAddAnyPatientInMemBodyTemp_ThenCorrectRisesValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Testowy");
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
        public void WhenAddAnyPatientInMemBodyTemp_ThenCorrectNotRisesValue()
        {
            // Arrange
            var patientMemory = new PatientInMemory("Jan Testowy");
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
    }
}