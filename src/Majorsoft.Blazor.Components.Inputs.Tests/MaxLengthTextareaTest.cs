using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Bunit;

using Moq;

namespace Majorsoft.Blazor.Components.Inputs.Tests
{
	[TestClass]
	public class MaxLengthTextareaTest
	{
		private Bunit.TestContext _testContext;

		[TestInitialize]
		public void Init()
		{
			_testContext = new Bunit.TestContext();

			var logger = new Mock<ILogger<MaxLengthTextarea>>();

			_testContext.Services.Add(new ServiceDescriptor(typeof(ILogger<MaxLengthTextarea>), logger.Object));
		}

		[TestCleanup]
		public void Cleanup()
		{
			_testContext?.Dispose();
		}

		[TestMethod]
		public void MaxLengthTextarea_should_rendered_correctly_html_attributes()
		{
			var rendered = _testContext.RenderComponent<MaxLengthTextarea>(
				("id", "id1"), //HTML attributes
				("class", "form-control w-100") //HTML attributes
				);

			var input = rendered.Find("textarea");
			var label = rendered.Find("label");

			Assert.IsNotNull(input);
			Assert.IsNotNull(label);
			input.MarkupMatches(@"<textarea maxlength=""50""  id=""id1"" class=""form-control w-100"" ></textarea>");
			label.MarkupMatches(@"<label>Remaining characters: 50</label>");
		}

		[TestMethod]
		public void MaxLengthTextarea_should_rendered_initial_value()
		{
			var rendered = _testContext.RenderComponent<MaxLengthTextarea>(parameters => parameters
				.Add(p => p.Value, "test"));

			var input = rendered.Find("textarea");
			var label = rendered.Find("label");

			Assert.IsNotNull(input);
			Assert.IsNotNull(label);
			input.MarkupMatches(@"<textarea value=""test"" maxlength=""50""/>");
			label.MarkupMatches(@"<label>Remaining characters: 46</label>");
		}

		[TestMethod]
		public void MaxLengthTextarea_should_rendered_initial_value_with_countdown_text()
		{
			var rendered = _testContext.RenderComponent<MaxLengthTextarea>(parameters => parameters
				.Add(p => p.Value, "test")
				.Add(p => p.CountdownText, "Remaining chars: "));

			var input = rendered.Find("textarea");
			var label = rendered.Find("label");

			Assert.IsNotNull(input);
			Assert.IsNotNull(label);
			input.MarkupMatches(@"<textarea value=""test"" maxlength=""50""/>");
			label.MarkupMatches(@"<label>Remaining chars: 46</label>");
		}

		[TestMethod]
		public void MaxLengthTextarea_should_rendered_initial_MaxAllowedChars()
		{
			var rendered = _testContext.RenderComponent<MaxLengthTextarea>(parameters => parameters
				.Add(p => p.MaxAllowedChars, 11));

			var input = rendered.Find("textarea");
			var label = rendered.Find("label");

			Assert.IsNotNull(input);
			Assert.IsNotNull(label);
			input.MarkupMatches(@"<textarea maxlength=""11""/>");
			label.MarkupMatches(@"<label>Remaining characters: 11</label>");
		}

		[TestMethod]
		public void MaxLengthTextarea_should_rendered_initial_CountdownTextClass()
		{
			var rendered = _testContext.RenderComponent<MaxLengthTextarea>(parameters => parameters
				.Add(p => p.CountdownTextClass, "css1 css2"));

			var input = rendered.Find("textarea");
			var label = rendered.Find("label");

			Assert.IsNotNull(input);
			Assert.IsNotNull(label);
			input.MarkupMatches(@"<textarea maxlength=""50""/>");
			label.MarkupMatches(@"<label class=""css1 css2"">Remaining characters: 50</label>");
		}

		[TestMethod]
		public void MaxLengthTextarea_should_rendered_and_event_triggered()
		{
			string text = string.Empty;
			int remaining = 0;

			var rendered = _testContext.RenderComponent<MaxLengthTextarea>(parameters => parameters
				.Add(p => p.OnInput, val => { text = val; })
				.Add(p => p.OnRemainingCharsChanged, val => { remaining = val; }));

			var input = rendered.Find("textarea");
			var label = rendered.Find("label");

			input.Input("t");

			Assert.IsNotNull(input);
			Assert.IsNotNull(label);
			Assert.AreEqual("t", text);
			Assert.AreEqual(49, remaining);

			input.MarkupMatches(@"<textarea value=""t"" maxlength=""50""/>");
			label.MarkupMatches(@"<label>Remaining characters: 49</label>");
		}
	}
}