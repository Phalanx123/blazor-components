using System;
using System.Threading.Tasks;

using Bunit;

using Majorsoft.Blazor.Components.CommonTestsBase;
using Majorsoft.Blazor.Components.Timer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Majorsoft.Blazor.Components.Debounce.Tests
{
	[TestClass]
	public class DebounceInputTest : ComponentsTestBase<DebounceInput>
	{
		[TestInitialize]
		public void Init()
		{
			var logger2 = new Mock<ILogger<AdvancedTimer>>();
			
			_testContext.Services.Add(new ServiceDescriptor(typeof(ILogger<AdvancedTimer>), logger2.Object));
		}

		[TestMethod]
		public void DebounceInput_should_rendered_correctly_html_attributes()
		{
			var rendered = _testContext.RenderComponent<DebounceInput>(
				("id", "id1"), //HTML attributes
				("class", "form-control w-100") //HTML attributes
				);

			var input = rendered.Find("input");

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input id=""id1"" class=""form-control w-100"" />");
		}

		[TestMethod]
		public void DebounceInput_should_rendered_initial_value()
		{
			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "test"));

			var input = rendered.Find("input");

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""test""/>");
		}

		[TestMethod]
		public async Task DebounceInput_should_wait_debounce_time()
		{
			var debounceTime = 50;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(debounceTime * 4); //wait for debounce
			rendered.WaitForAssertion(() => 
			{
				Assert.AreEqual("c", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue((valueEventTime - inputTime) >= TimeSpan.FromMilliseconds(debounceTime * 0.9));
			}, timeout: TimeSpan.FromSeconds(1));
		}

		[TestMethod]
		public async Task DebounceInput_should_wait_debounce_time_notify_empty_below_minChars()
		{
			var debounceTime = 200;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.MinLength, 2)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(debounceTime * 4); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue((valueEventTime - inputTime) >= TimeSpan.FromMilliseconds(debounceTime * 0.9));
			}, timeout: TimeSpan.FromSeconds(1));
		}


		[TestMethod]
		public async Task DebounceInput_should_wait_debounce_time_respective_to_minChars()
		{
			var debounceTime = 50;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.MinLength, 2)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("cat");
			var inputTime = DateTime.Now;

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""cat""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(debounceTime * 4); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("cat", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue((valueEventTime - inputTime) >= TimeSpan.FromMilliseconds(debounceTime * 0.9));
			}, timeout: TimeSpan.FromSeconds(1));
		}

		[TestMethod]
		public async Task DebounceInput_should_not_wait_debounce_time_on_force_enter()
		{
			var debounceTime = 500;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;
			input.KeyPress("Enter");

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(50); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("c", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue(valueEventTime - inputTime < TimeSpan.FromMilliseconds(debounceTime));
			}, timeout: TimeSpan.FromSeconds(1));
		}

		[TestMethod]
		public async Task DebounceInput_should_not_wait_debounce_time_on_force_enter_notify_empty_below_minChars()
		{
			var debounceTime = 500;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.MinLength, 2)
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;
			input.KeyPress("Enter");

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(50); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue(valueEventTime - inputTime < TimeSpan.FromMilliseconds(debounceTime));
			}, timeout: TimeSpan.FromSeconds(1));
		}

		[TestMethod]
		public async Task DebounceInput_should_wait_debounce_time_on_disabled_force_enter()
		{
			var debounceTime = 50;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.ForceNotifyByEnter, false)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;
			input.KeyPress("Enter");

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(debounceTime * 4); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("c", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue((valueEventTime - inputTime) >= TimeSpan.FromMilliseconds(debounceTime * 0.9));
			}, timeout: TimeSpan.FromSeconds(1));
		}


		[TestMethod]
		public async Task DebounceInput_should_not_wait_debounce_time_on_force_blur()
		{
			var debounceTime = 500;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;
			input.Blur();

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(50); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("c", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue(valueEventTime - inputTime < TimeSpan.FromMilliseconds(debounceTime));
			}, timeout: TimeSpan.FromSeconds(1));
		}

		[TestMethod]
		public async Task DebounceInput_should_not_wait_debounce_time_on_force_blur_notify_empty_below_minChars()
		{
			var debounceTime = 500;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.MinLength, 2)
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;
			input.Blur();

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(50); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue(valueEventTime - inputTime < TimeSpan.FromMilliseconds(debounceTime));
			}, timeout: TimeSpan.FromSeconds(1));
		}


		[TestMethod]
		public async Task DebounceInput_should_wait_debounce_time_on_disabled_force_blur()
		{
			var debounceTime = 50;
			DateTime eventTime = DateTime.MinValue;
			DateTime valueEventTime = DateTime.MinValue;
			string notifiedValue = null;

			var rendered = _testContext.RenderComponent<DebounceInput>(parameters => parameters
				.Add(p => p.Value, "")
				.Add(p => p.ForceNotifyOnBlur, false)
				.Add(p => p.DebounceTime, debounceTime)
				.Add(p => p.OnInput, val => { eventTime = DateTime.Now; })
				.Add(p => p.OnValueChanged, val => { valueEventTime = DateTime.Now; notifiedValue = val; }));

			var input = rendered.Find("input");
			input.Input("c");
			var inputTime = DateTime.Now;
			input.Blur();

			Assert.IsNotNull(input);
			input.MarkupMatches(@"<input value=""c""/>"); //value updated immediately with event
			Assert.IsTrue(eventTime > DateTime.MinValue);

			await Task.Delay(debounceTime * 4); //wait for debounce
			rendered.WaitForAssertion(() =>
			{
				Assert.AreEqual("c", notifiedValue);
				Assert.IsTrue(valueEventTime > DateTime.MinValue);
				Assert.IsTrue((valueEventTime - inputTime) >= TimeSpan.FromMilliseconds(debounceTime * 0.9));
			}, timeout: TimeSpan.FromSeconds(1));
		}
	}
}