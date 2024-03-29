﻿namespace WhatsON.Plugins.Travis.Tests
{
  using NUnit.Framework;
  using System.Threading.Tasks;

  /// <summary>
  /// In order to execute these tests against the real Travis CI server, you need to have the environment variable TRAVIS_AUTH_TOKEN set for your user to a valid authentication token.
  /// For details on how the tokens work, please see https://developer.travis-ci.com/authentication.
  /// </summary>
  /// <see cref="TravisAPI.ENV_TRAVIS_AUTH_TOKEN"/>
  /// <see cref="TravisAPI.FetchTravisToken"/>
  [TestFixture]
  public class TravisAPITests
  {
    [Test]
    public async Task TestFetchProjects()
    {
      var jobContainer = await TravisAPI.GetJobs("https://travis-ci.com/gurkenlabs/litiengine");
      Assert.IsNotNull(jobContainer);
      Assert.IsTrue(jobContainer.Count > 0);
    }

    [Test]
    public async Task TestFetchRepositories()
    {
      var repositoryContainer = await TravisAPI.GetRepositories("https://travis-ci.com/gurkenlabs");
      Assert.IsNotNull(repositoryContainer);
      Assert.IsTrue(repositoryContainer.Count > 0);
    }

    [Test]
    public async Task TestFetchHistoryBuilds()
    {
      var builds = await TravisAPI.GetBuilds("gurkenlabs", "litiengine", "master");
      Assert.IsNotNull(builds);
      Assert.AreEqual(5, builds.Count);
    } 

    [Test]
    public void TestSlugEvaluation()
    {
      Assert.AreEqual("gurkenlabs", TravisAPI.GetOwnerName("gurkenlabs/litiengine"));
      Assert.AreEqual("gurkenlabs", TravisAPI.GetOwnerName("gurkenlabs/litiengine/"));

      Assert.AreEqual("litiengine", TravisAPI.GetRepositoryName("gurkenlabs/litiengine"));
      Assert.AreEqual("litiengine", TravisAPI.GetRepositoryName("gurkenlabs/litiengine/"));

      Assert.AreEqual("gurkenlabs/litiengine", TravisAPI.GetSlug("https://travis-ci.com/gurkenlabs/litiengine"));
      Assert.AreEqual("gurkenlabs/litiengine", TravisAPI.GetSlug("https://travis-ci.com/gurkenlabs/litiengine/"));

      Assert.AreEqual("gurkenlabs/litiengine", TravisAPI.GetSlug("https://travis-ci.com/gurkenlabs/litiengine/asdsaldiahjsdnmasdöasdsadasödlasuidja"));
    }
  }
}
